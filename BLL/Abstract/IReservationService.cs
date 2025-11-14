using BLL.Models;

namespace BLL.Abstract
{
    public interface IReservationService
    {
        Task<int> CreateReservationAsync(PostReservationRequestModel requestModel);
    }
}
