using AutoMapper;
using BLL.Abstract;
using BLL.Models;
using DAL.Data;
using DAL.Entities;
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
    }
}
