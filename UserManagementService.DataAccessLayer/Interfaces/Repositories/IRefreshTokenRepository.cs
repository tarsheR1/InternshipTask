using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenEntity> GetByIdAsync(Guid id, CancellationToken cancellation);
        Task<RefreshTokenEntity> GetByTokenAsync(string token, CancellationToken cancellation);
        Task<IEnumerable<RefreshTokenEntity>> GetByUserIdAsync(Guid userId, CancellationToken cancellation);

        Task CreateAsync(RefreshTokenEntity token, CancellationToken cancellation);
        Task UpdateAsync(RefreshTokenEntity token, CancellationToken cancellation);
        Task RevokeAsync(Guid id, DateTime revokedAt, CancellationToken cancellation);

        Task<bool> ExistsActiveTokenAsync(Guid userId, CancellationToken cancellation);
        Task DeleteExpiredTokensAsync(CancellationToken cancellation);
    }
}
