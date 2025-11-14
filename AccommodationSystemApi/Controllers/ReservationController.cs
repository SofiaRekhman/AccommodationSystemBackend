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
    }
}
