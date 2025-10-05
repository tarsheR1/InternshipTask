using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Entities;
using UserManagementService.DataAccessLayer.Interfaces.Repositories;
using UserManagementService.DataAccessLayer.Persistence;

namespace UserManagementService.DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagementDbContext _context;

        public UserRepository(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> GetByIdAsync(Guid userId, CancellationToken cancellation)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellation)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task AddAsync(UserEntity user, CancellationToken cancellation)
        {
            await _context.Users.AddAsync(user, cancellation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserEntity user, CancellationToken cancellation)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellation);
        }

        public async Task DeleteAsync(UserEntity user, CancellationToken cancellation)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellation);
        }

        public async Task<List<string>> GetUserRolesAsync(Guid userId, CancellationToken cancellation)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return new List<string>();

            return user.UserRoles
                .Select(ur => ur.Role.Name)
                .ToList();
        }
    }
}
