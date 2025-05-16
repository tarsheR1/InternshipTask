using UserManagementService.BusinessLogicLayer.Models.DTO.Request.Auth;
using UserManagementService.BusinessLogicLayer.Models.DTO.Response;
using UserManagementService.BusinessLogicLayer.Models.Queries;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<RegisterUserResponseDto> RegisterAsync(RegisterUserRequestDto registerRequest, CancellationToken cancellationToken);
        Task<AuthResult> LoginAsync(LoginRequestDto loginRequest, CancellationToken cancellationToken);
        Task RevokeTokenAsync(string refreshToken, CancellationToken cancellation);
        Task<AuthResult> RefreshTokenAsync(string refreshToken, Guid userId, CancellationToken cancellation);
        Task<string> GenerateEmailConfirmationTokenAsync(Guid userId, CancellationToken cancellationToken);
        Task<bool> ConfirmEmailAsync(Guid userId, string token, CancellationToken cancellationToken)

    }
};
