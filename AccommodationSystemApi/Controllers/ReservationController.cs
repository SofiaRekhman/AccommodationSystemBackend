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
            PostReservationResponseModel responseModel = await _reservationService.CreateReservationAsync(requestModel);

            return Ok(responseModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetReservationsAsync()
        {
            List<GetReservationsResponseModel> reservations = await _reservationService.GetReservationsAsync();

            if (reservations == null)
            {
                return NotFound();
            }

            return Ok(reservations);
        }

        [HttpGet("{reservation_id}")]
        public async Task<IActionResult> GetReservationByIdAsync(int reservation_id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(reservation_id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }
    }
}
