using BLL.Abstract;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationSystemApi.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservationAsync(PostReservationRequestModel requestModel)
        {
            int reservationId = await _reservationService.CreateReservationAsync(requestModel);

            return Ok(reservationId);
        }

        [HttpGet("{reservation_id}")]
        public async Task<IActionResult> GetReservationByIdAsync(int reservation_id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(reservation_id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                full_name = reservation.FullName,
                phone_number = reservation.PhoneNumber,
                room_id = reservation.RoomId,
                bed_id = reservation.BedId,
                reservation_start_date = reservation.ReservationStartDate,
                reservation_end_date = reservation.ReservationEndDate
            });
        }
    }
}
