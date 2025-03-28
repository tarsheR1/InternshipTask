using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Models.Queries;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(UserRegistrationCommand registerRequest, CancellationToken cancellationToken);
        Task<string> LoginAsync(UserLoginCommand loginRequest, CancellationToken cancellationToken);
        Task RevokeTokenAsync(AuthResult authResult, CancellationToken cancellation);
        Task<AuthResult> RefreshTokenAsync(AuthResult authResult, CancellationToken cancellation);
    }
};
