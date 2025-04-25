using UserManagementService.DataAccessLayer.Entities.Users;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Base;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories.Users
{
    public interface IRefreshTokenRepository : IBaseRepository<RefreshTokenEntity, Guid>
    {
        Task<RefreshTokenEntity> GetByTokenAsync(string token, CancellationToken cancellation);
        Task<List<RefreshTokenEntity>> GetByUserIdAsync(Guid userId, CancellationToken cancellation);

        //Task RevokeAsync(Guid id, DateTime revokedAt, CancellationToken cancellation);

        Task<bool> ExistsActiveTokenAsync(Guid userId, CancellationToken cancellation);
        Task DeleteExpiredTokensAsync(CancellationToken cancellation);
    }
}
