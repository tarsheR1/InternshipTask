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

        public async Task<(List<UserEntity> Users, int TotalCount)> GetPagedUsersAsync(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var query = _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .AsNoTracking();

            int totalCount = await query.CountAsync(cancellationToken);

            var users = await query
                .OrderBy(u => u.Email) 
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (users, totalCount);
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
                .ThenInclude(rp => rp.RolePermissions)
                .ThenInclude(p => p.Permission)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(UserEntity user, CancellationToken cancellation)
        {
            await _context.Users.AddAsync(user, cancellation);
        }

        public async Task UpdateAsync(UserEntity user, CancellationToken cancellation)
        {
            _context.Users.Update(user);
        }

        public async Task DeleteAsync(UserEntity user, CancellationToken cancellation)
        {
            _context.Users.Remove(user);
        }

        public async Task<List<string>> GetUserRolesAsync(Guid userId, CancellationToken cancellation)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user.UserRoles
                .Select(ur => ur.Role.Name)
                .ToList();
        }
    }
}
