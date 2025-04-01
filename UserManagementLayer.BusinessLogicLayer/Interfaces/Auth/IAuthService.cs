using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Models.Queries;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(UserRegistrationCommand registerRequest, CancellationToken cancellationToken);
        Task<AuthResult> LoginAsync(UserLoginCommand loginRequest, CancellationToken cancellationToken);
        Task RevokeTokenAsync(string refreshToken, CancellationToken cancellation);
        Task<AuthResult> RefreshTokenAsync(string refreshToken, Guid userId, CancellationToken cancellation);
    }
};
