using UserManagementService.BusinessLogicLayer.Models.Entities.Auth;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Auth
{
    public interface IRefreshTokenService
    {
        Task<string> GenerateRefreshTokenAsync(Guid userId, CancellationToken cancellation);
        Task<RefreshToken> GetRefreshTokenAsync(string tokenString, CancellationToken cancellation);
        Task<bool> ValidateRefreshTokenAsync(RefreshToken token, CancellationToken cancellation);
        Task RevokeRefreshTokenAsync(string token, CancellationToken cancellation);
    }
}
