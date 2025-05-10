using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Entities.Role;
using UserManagementService.DataAccessLayer.Repositories.Base;
using UserManagementService.DataAccessLayer.Persistence;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Roles;
using UserManagementService.DataAccessLayer.Entities.Users;

namespace UserManagementService.DataAccessLayer.Repositories.Roles
{
    public class RoleRepository : BaseRepository<RoleEntity, int>, IRoleRepository
    {
        private readonly UserManagementDbContext _context;

        public RoleRepository(UserManagementDbContext context) : base(context) { }

        public async Task<List<RoleEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Roles.ToListAsync(cancellationToken);
        }

        public async Task<RoleEntity> GetByNameAsync(string roleName, CancellationToken cancellationToken)
        {
            return await _context.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
        }

        public async Task<List<UserEntity>> GetUsersForRoleAsync(int roleId, CancellationToken cancellationToken)
        {
            return await _context.Users
              .Where(u => u.Roles.Any(r => r.Id == roleId))
              .ToListAsync(cancellationToken);
        }

        public async Task<List<PermissionEntity>> GetPermissionsForRoleAsync(int roleId, CancellationToken cancellationToken = default)
        {
            return await _context.Permissions
                .Where(r => r.Id == roleId)
                .Include(r => r.Roles)
                .ToListAsync(cancellationToken);
        }
    }
}
