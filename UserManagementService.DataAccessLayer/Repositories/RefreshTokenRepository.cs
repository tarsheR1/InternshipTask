using UserManagementService.DataAccessLayer.Persistence;
using UserManagementService.DataAccessLayer.Entities;
using UserManagementService.DataAccessLayer.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UserManagementService.DataAccessLayer.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly UserManagementDbContext _context;

        public RefreshTokenRepository(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(RefreshTokenEntity token, CancellationToken cancellation)
        {
            await _context.RefreshTokens.AddAsync(token, cancellation);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshTokenEntity> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            return await _context.RefreshTokens.FindAsync(id);
        }

        public async Task<RefreshTokenEntity> GetByTokenAsync(string token, CancellationToken cancellation)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task<List<RefreshTokenEntity>> GetByUserIdAsync(Guid userId, CancellationToken cancellation)
        {
            return await _context.RefreshTokens
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateAsync(RefreshTokenEntity token, CancellationToken cancellation)
        {
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task RevokeAsync(Guid id, DateTime revokedAt, CancellationToken cancellation)
        {
            var token = await GetByIdAsync(id, cancellation);
            if (token != null)
            {
                token.Revoked = revokedAt;
                await UpdateAsync(token, cancellation);
            }
        }

        public async Task<bool> ExistsActiveTokenAsync(Guid userId, CancellationToken cancellation)
        {
            return await _context.RefreshTokens
                .AnyAsync(x => x.UserId == userId &&
                             x.Revoked == null &&
                             x.Expires > DateTime.UtcNow);
        }

        public async Task DeleteExpiredTokensAsync(CancellationToken cancellation)
        {
            var expiredTokens = _context.RefreshTokens
                .Where(x => x.Expires < DateTime.UtcNow);

            _context.RefreshTokens.RemoveRange(expiredTokens);
            await _context.SaveChangesAsync();
        }
    }
}
