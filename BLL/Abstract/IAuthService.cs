using BLL.Models;

namespace BLL.Abstract
{
    public interface IAuthService
    {
        Task<LoginResponseModel> LoginAsync(LoginRequestModel request);
        Task<bool> RegisterAsync(RegisterRequestModel request);
        Task<bool> LogoutAsync();
    }
}

