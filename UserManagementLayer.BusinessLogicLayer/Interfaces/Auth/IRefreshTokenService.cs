namespace UserManagementService.BusinessLogicLayer.Interfaces.Auth
{
    public interface IRefreshTokenService
    {
        Task<string> GenerateRefreshTokenAsync(Guid userId, CancellationToken cancellation);
        Task<bool> ValidateRefreshTokenAsync(string token, CancellationToken cancellation);
        Task RevokeRefreshTokenAsync(string token, CancellationToken cancellation);
    }
}
