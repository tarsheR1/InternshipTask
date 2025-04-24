using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Entities.Relations;
using UserManagementService.DataAccessLayer.Entities.Role;
using UserManagementService.DataAccessLayer.Entities.Users;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Relations;
using UserManagementService.DataAccessLayer.Persistence;

namespace UserManagementService.DataAccessLayer.Repositories.Relations
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly UserManagementDbContext _context;

        public UserRoleRepository(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task<UserRoleEntity> GetAsync(Guid userId, int roleId, CancellationToken cancellationToken)
        {
            return await _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);
        }

        public async Task<List<UserRoleEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(UserRoleEntity userRoleAssign, CancellationToken cancellationToken)
        {
            await _context.UserRoles.AddAsync(userRoleAssign, cancellationToken);
        }

        public async Task DeleteAsync(UserRoleEntity userRole, CancellationToken cancellationToken)
        {
             _context.UserRoles.Remove(userRole);
        }

        public async Task<List<RoleEntity>> GetRolesForUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Role)
                .Select(ur => ur.Role)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<UserEntity>> GetUsersForRoleAsync(int roleId, CancellationToken cancellationToken)
        {
            return await _context.UserRoles
                .Where(ur => ur.RoleId == roleId)
                .Include(ur => ur.User)
                .Select(ur => ur.User)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid userId, int roleId, CancellationToken cancellationToken)
        {
            return await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);
        }

        public async Task<bool> UserHasAnyRoleAsync(Guid userId, IEnumerable<int> roleIds, CancellationToken cancellationToken)
        {
            return await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && roleIds.Contains(ur.RoleId), cancellationToken);
        }
    }
}
