using BLL.Models;

namespace BLL.Abstract
{
    public interface IReservationService
    {
        Task<int> CreateReservationAsync(PostReservationRequestModel requestModel);
        Task<GetReservationResponseModel?> GetReservationByIdAsync(int reservationId);
    }
}
