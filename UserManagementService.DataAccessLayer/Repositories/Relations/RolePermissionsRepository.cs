using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Entities.Relations;
using UserManagementService.DataAccessLayer.Entities.Role;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Relations;
using UserManagementService.DataAccessLayer.Persistence;

namespace UserManagementService.DataAccessLayer.Repositories.Relations
{
    public class RolePermissionsRepository : IRolePermissionRepository
    {
        private readonly UserManagementDbContext _context;

        public RolePermissionsRepository(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task<RolePermissionEntity> GetAsync(int roleId, int permissionId, CancellationToken cancellationToken = default)
        {
            return await _context.RolePermissions
                .Include(rp => rp.Role)
                .Include(rp => rp.Permission)
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId, cancellationToken);
        }

        public async Task<IEnumerable<RolePermissionEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.RolePermissions
                .Include(rp => rp.Role)
                .Include(rp => rp.Permission)
                .ToListAsync(cancellationToken);
        }
         
        public async Task AddAsync(RolePermissionEntity rolePermission, CancellationToken cancellationToken = default)
        {
            await _context.RolePermissions.AddAsync(rolePermission, cancellationToken);
        }

        public async Task DeleteAsync(int roleId, int permissionId, CancellationToken cancellationToken = default)
        {
            var entity = await GetAsync(roleId, permissionId, cancellationToken);
            if (entity != null)
            {
                _context.RolePermissions.Remove(entity);
            }
        }

        public async Task<bool> ExistsAsync(int roleId, int permissionId, CancellationToken cancellationToken = default)
        {
            return await _context.RolePermissions
                .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId, cancellationToken);
        }

        public async Task<IEnumerable<PermissionEntity>> GetPermissionsForRoleAsync(int roleId, CancellationToken cancellationToken = default)
        {
            return await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Include(rp => rp.Permission)
                .Select(rp => rp.Permission)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<RoleEntity>> GetRolesForPermissionAsync(int permissionId, CancellationToken cancellationToken)
        {
            return await _context.RolePermissions
                .Where(rp => rp.PermissionId == permissionId)
                .Include(rp => rp.Role)
                .Select(rp => rp.Role)
                .ToListAsync(cancellationToken);
        }

        public async Task AddPermissionToRoleAsync(int roleId, int permissionId, CancellationToken cancellationToken)
        {
            if (!await ExistsAsync(roleId, permissionId))
            {
                await AddAsync(new RolePermissionEntity
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                }, cancellationToken);
            }
        }

        public async Task RemovePermissionFromRoleAsync(
            int roleId, 
            int permissionId, 
            CancellationToken cancellationToken = default)
        {
            await DeleteAsync(roleId, permissionId, cancellationToken);
        }
    }
}
