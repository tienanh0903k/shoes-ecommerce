using ShoesStoreApp.BLL.ViewModels.Auth;

namespace ShoesStoreApp.BLL.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<string> RegisterUserAsync(RegisterVm registerVm);
        Task<AuthResultVm> RefreshTokenAsync(string refreshToken);
        Task<AuthResultVm> LoginUserAsync(LoginVm loginVm);

        Task<UserVm> GetUserInfoAsync(Guid userId);
        Task<bool> UpdateUserInfoAsync(Guid userId, UpdateUserVm updateUserVm);
        Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordVm passwordVm);

    }
}
