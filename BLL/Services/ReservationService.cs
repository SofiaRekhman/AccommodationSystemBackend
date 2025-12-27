using AutoMapper;
using BLL.Abstract;
using BLL.Models;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Status = DAL.Enums.Status;

namespace BLL.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public ReservationService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PostReservationResponseModel> CreateReservationAsync(PostReservationRequestModel requestModel)
        {
            Reservation reservation = _mapper.Map<Reservation>(requestModel);

            reservation.StatusId = (int)Status.Created;

            _dbContext.Reservations.Add(reservation);
            await _dbContext.SaveChangesAsync();

            return new PostReservationResponseModel { ReservationId = reservation.ReservationId };
        }

        public async Task<List<GetReservationsResponseModel>> GetReservationsAsync(int? userId, int? statusId, string? sortBy)
        {
            IQueryable<Reservation> query = _dbContext.Reservations
                .Include(x => x.Status)
                .Where(x => userId == null || x.UserId == userId)
                .Where(x => statusId == null || x.StatusId == statusId);

            query = sortBy switch
            {
                "dateAsc" => query.OrderBy(x => x.StartDate),
                "createdDesc" => query.OrderByDescending(x => x.CreatedAt),
                "createdAsc" => query.OrderBy(x => x.CreatedAt),
                _ => query.OrderByDescending(x => x.StartDate),
            };

            List<Reservation> reservations = await query.ToListAsync();

            return _mapper.Map<List<GetReservationsResponseModel>>(reservations);
        }


        public async Task<GetReservationResponseModel?> GetReservationByIdAsync(int reservationId)
        {
            var reservation = await _dbContext.Reservations
                .Include(r => r.User)
                .Include(r => r.Status)
                .Include(r => r.Bed)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

            if (reservation == null)
            {
                return null;
            }

            var fullName = string.Empty;
            if (reservation.User != null)
            {
                var nameParts = new List<string> { reservation.User.Name, reservation.User.Surname };
                fullName = string.Join(" ", nameParts);
            }

            return new GetReservationResponseModel
            {
                FullName = fullName,
                PhoneNumber = reservation.User?.PhoneNumber ?? string.Empty,
                RoomNumber = reservation.Room.RoomNumber,
                BedNumber = reservation.Bed.BedNumber,
                ReservationStartDate = reservation.StartDate.ToString("yyyy-MM-dd"),
                ReservationEndDate = reservation.EndDate.ToString("yyyy-MM-dd"),
                ReservationStatusName = reservation.Status.StatusName,
                Reason = reservation.Reason,
                AdminComment = reservation.AdminComment
            };
        }

        public async Task<int?> UpdateReservationAsync(int reservationId, PutReservationRequestModel requestModel)
        {
            var reservation = await _dbContext.Reservations
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

            if (reservation == null)
            {
                return null;
            }

            reservation.StatusId = requestModel.StatusId;
            reservation.AdminComment = requestModel.AdminComment;
            reservation.UpdatedAt = DateTime.UtcNow;

            _dbContext.Reservations.Update(reservation);
            await _dbContext.SaveChangesAsync();

            return reservationId;
        }

        public async Task<List<RoomAvailabilityResponseDto>> GetAvailableRoomsWithBedsAsync(DateTime startDate, DateTime endDate)
        {
            List<int> occupiedBedIds = await _dbContext.Reservations
                .Where(r => r.StartDate <= endDate && r.EndDate >= startDate)
                .Select(r => r.BedId)
                .ToListAsync();

            List<RoomAvailabilityResponseDto> availableData = await _dbContext.Rooms
                .Select(r => new RoomAvailabilityResponseDto
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    AvailableBeds = r.Beds
                        .Where(b => !occupiedBedIds.Contains(b.BedId))
                        .ToList()
                })
                .Where(r => r.AvailableBeds.Any())
                .OrderBy(r => r.RoomNumber)
                .ToListAsync();

            return availableData;
        }
    }
}
