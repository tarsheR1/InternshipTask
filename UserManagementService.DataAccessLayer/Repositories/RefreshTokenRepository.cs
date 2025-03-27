using UserManagementService.DataAccessLayer.Persistence;
using UserManagementService.DataAccessLayer.Entities;
using UserManagementService.DataAccessLayer.Interfaces;
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

        public async Task CreateAsync(RefreshTokenEntity token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshTokenEntity> GetByIdAsync(int id)
        {
            return await _context.RefreshTokens.FindAsync(id);
        }

        public async Task<RefreshTokenEntity> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task<IEnumerable<RefreshTokenEntity>> GetByUserIdAsync(Guid userId)
        {
            return await _context.RefreshTokens
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateAsync(RefreshTokenEntity token)
        {
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task RevokeAsync(int id, DateTime revokedAt)
        {
            var token = await GetByIdAsync(id);
            if (token != null)
            {
                token.Revoked = revokedAt;
                await UpdateAsync(token);
            }
        }

        public async Task<bool> ExistsActiveTokenAsync(Guid userId)
        {
            return await _context.RefreshTokens
                .AnyAsync(x => x.UserId == userId &&
                             x.Revoked == null &&
                             x.Expires > DateTime.UtcNow);
        }

        public async Task DeleteExpiredTokensAsync()
        {
            var expiredTokens = _context.RefreshTokens
                .Where(x => x.Expires < DateTime.UtcNow);

            _context.RefreshTokens.RemoveRange(expiredTokens);
            await _context.SaveChangesAsync();
        }
    }
}
