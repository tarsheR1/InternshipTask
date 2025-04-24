using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Persistence;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Users;
using System.Threading;
using UserManagementService.DataAccessLayer.Entities.Users;

namespace UserManagementService.DataAccessLayer.Repositories.Users
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly UserManagementDbContext _context;

        public RefreshTokenRepository(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshTokenEntity token, CancellationToken cancellationToken)
        {
            await _context.RefreshTokens.AddAsync(token, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<RefreshTokenEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens.FindAsync(id, cancellationToken);
        }

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

        public async Task UpdateAsync(RefreshTokenEntity token, CancellationToken cancellationToken)
        {
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RevokeAsync(Guid id, DateTime revokedAt, CancellationToken cancellationToken)
        {
            var token = await GetByIdAsync(id, cancellationToken);
            if (token != null)
            {
                token.Revoked = revokedAt;
                await UpdateAsync(token, cancellationToken);
            }
        }

        public async Task<bool> ExistsActiveTokenAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens
                .AnyAsync(x => x.UserId == userId &&
                             x.Revoked == null &&
                             x.Expires > DateTime.UtcNow,
                             cancellationToken);
        }

        public async Task DeleteExpiredTokensAsync(CancellationToken cancellationToken)
        {
            var expiredTokens = _context.RefreshTokens
                .Where(x => x.Expires < DateTime.UtcNow);

            _context.RefreshTokens.RemoveRange(expiredTokens);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(RefreshTokenEntity refreshToken, CancellationToken cancellationToken)
        {
            _context.Remove(refreshToken);
        }
    }
}
