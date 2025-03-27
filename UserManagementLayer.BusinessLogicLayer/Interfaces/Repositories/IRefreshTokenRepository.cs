using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenEntity> GetByIdAsync(int id);
        Task<RefreshTokenEntity> GetByTokenAsync(string token);
        Task<IEnumerable<RefreshTokenEntity>> GetByUserIdAsync(Guid userId);

        Task CreateAsync(RefreshTokenEntity token);
        Task UpdateAsync(RefreshTokenEntity token);
        Task RevokeAsync(int id, DateTime revokedAt);

        Task<bool> ExistsActiveTokenAsync(Guid userId);
        Task DeleteExpiredTokensAsync();
    }
}
