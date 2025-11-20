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

        public async Task<int> CreateReservationAsync(PostReservationRequestModel requestModel)
        {
            Reservation reservation = _mapper.Map<Reservation>(requestModel);

            reservation.StatusId = (int)Status.Created;

            _dbContext.Reservations.Add(reservation);
            await _dbContext.SaveChangesAsync();

            return reservation.ReservationId;
        }

        public async Task<List<GetReservationsResponseModel>> GetReservationsAsync()
        {
            List<Reservation> reservations = await _dbContext.Reservations.ToListAsync();

            return _mapper.Map<List<GetReservationsResponseModel>>(reservations);
        }

        public async Task<GetReservationResponseModel?> GetReservationByIdAsync(int reservationId)
        {
            var reservation = await _dbContext.Reservations
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

            if (reservation == null)
            {
                return null;
            }

            var fullName = string.Empty;
            if (reservation.User != null)
            {
                var nameParts = new List<string> { reservation.User.Name, reservation.User.Surname };
                if (!string.IsNullOrEmpty(reservation.User.Patronymic))
                {
                    nameParts.Add(reservation.User.Patronymic);
                }
                fullName = string.Join(" ", nameParts);
            }

            return new GetReservationResponseModel
            {
                FullName = fullName,
                PhoneNumber = reservation.User?.PhoneNumber ?? string.Empty,
                RoomId = reservation.RoomId,
                BedId = 0, // bed_id не є частиною reservation в схемі БД
                ReservationStartDate = reservation.StartDate.ToString("yyyy-MM-dd"),
                ReservationEndDate = reservation.EndDate.ToString("yyyy-MM-dd")
            };
        }
    }
}
