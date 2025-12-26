using BLL.Models;

namespace BLL.Abstract
{
    public interface IReservationService
    {
        Task<PostReservationResponseModel> CreateReservationAsync(PostReservationRequestModel requestModel);
        Task<List<GetReservationsResponseModel>> GetReservationsAsync(int? userId, int? statusId, string? sortBy);
        Task<GetReservationResponseModel?> GetReservationByIdAsync(int reservationId);
        Task<int?> UpdateReservationAsync(int reservationId, PutReservationRequestModel requestModel);
        Task<List<RoomAvailabilityResponseDto>> GetAvailableRoomsWithBedsAsync(DateTime startDate, DateTime endDate);
    }
}
