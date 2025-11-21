using BLL.Models;

namespace BLL.Abstract
{
    public interface IReservationService
    {
        Task<PostReservationResponseModel> CreateReservationAsync(PostReservationRequestModel requestModel);
        Task<List<GetReservationsResponseModel>> GetReservationsAsync();
        Task<GetReservationResponseModel?> GetReservationByIdAsync(int reservationId);
    }
}
