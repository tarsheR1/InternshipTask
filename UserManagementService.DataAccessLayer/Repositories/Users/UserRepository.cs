using Microsoft.EntityFrameworkCore;
using Shared.Pagination;
using System.Threading;
using UserManagementService.DataAccessLayer.Entities.Users;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Users;
using UserManagementService.DataAccessLayer.Persistence;

namespace UserManagementService.DataAccessLayer.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagementDbContext _context;

        public UserRepository(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task<(List<UserEntity> Users, int TotalCount)> GetPagedAsync(
            PaginationParameters parameters,
            CancellationToken cancellationToken)
        {
            var query = _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .AsNoTracking();

            int totalCount = await query.CountAsync(cancellationToken);

            var users = await query
                .OrderBy(u => u.Email) 
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync(cancellationToken);

            return (users, totalCount);
        }

        public async Task<UserEntity> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ThenInclude(rp => rp.RolePermissions)
                .ThenInclude(p => p.Permission)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(UserEntity user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
        }

        public async Task UpdateAsync(UserEntity user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
        }

        public async Task DeleteAsync(UserEntity user, CancellationToken cancellationToken)
        {
            _context.Users.Remove(user);
        }

        public async Task<List<string>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken)
        {
            var roleNames = await _context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.UserRoles.Select(ur => ur.Role.Name))
                .ToListAsync(cancellationToken);

            return roleNames;
        }
    }
}
