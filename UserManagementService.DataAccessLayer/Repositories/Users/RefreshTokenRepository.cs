using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Persistence;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Users;
using System.Threading;
using UserManagementService.DataAccessLayer.Entities.Users;
using UserManagementService.DataAccessLayer.Repositories.Base;

namespace UserManagementService.DataAccessLayer.Repositories.Users
{
    public class RefreshTokenRepository : BaseRepository<RefreshTokenEntity, Guid>, IRefreshTokenRepository
    {
        private readonly UserManagementDbContext _context;

        public RefreshTokenRepository(UserManagementDbContext context) : base(context) { }

        public async Task<RefreshTokenEntity> GetByTokenAsync(string token, CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == token, cancellationToken);
        }

        public async Task<List<RefreshTokenEntity>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens
                .Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken);
        }
    }
}
