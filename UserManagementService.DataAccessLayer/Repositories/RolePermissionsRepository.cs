using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Entities;
using UserManagementService.DataAccessLayer.Interfaces.Repositories;
using UserManagementService.DataAccessLayer.Persistence;

namespace UserManagementService.DataAccessLayer.Repositories
{
    public class RolePermissionsRepository : IRolePermissionRepository
    {
        private readonly UserManagementDbContext _context;

        public RolePermissionsRepository(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task<RolePermissionEntity> GetAsync(int roleId, int permissionId)
        {
            return await _context.RolePermissions
                .Include(rp => rp.Role)
                .Include(rp => rp.Permission)
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
        }

        public async Task<IEnumerable<RolePermissionEntity>> GetAllAsync()
        {
            return await _context.RolePermissions
                .Include(rp => rp.Role)
                .Include(rp => rp.Permission)
                .ToListAsync();
        }

        public async Task AddAsync(RolePermissionEntity rolePermission)
        {
            await _context.RolePermissions.AddAsync(rolePermission);
        }

        public async Task DeleteAsync(int roleId, int permissionId)
        {
            var entity = await GetAsync(roleId, permissionId);
            if (entity != null)
            {
                _context.RolePermissions.Remove(entity);
            }
        }

        public async Task<bool> ExistsAsync(int roleId, int permissionId)
        {
            return await _context.RolePermissions
                .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
        }

        public async Task<IEnumerable<PermissionEntity>> GetPermissionsForRoleAsync(int roleId)
        {
            return await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Include(rp => rp.Permission)
                .Select(rp => rp.Permission)
                .ToListAsync();
        }

        public async Task<IEnumerable<RoleEntity>> GetRolesForPermissionAsync(int permissionId)
        {
            return await _context.RolePermissions
                .Where(rp => rp.PermissionId == permissionId)
                .Include(rp => rp.Role)
                .Select(rp => rp.Role)
                .ToListAsync();
        }

        public async Task AddPermissionToRoleAsync(int roleId, int permissionId)
        {
            if (!await ExistsAsync(roleId, permissionId))
            {
                await AddAsync(new RolePermissionEntity
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                });
            }
        }

        public async Task RemovePermissionFromRoleAsync(int roleId, int permissionId)
        {
            await DeleteAsync(roleId, permissionId);
        }

        public async Task UpdateRolePermissionsAsync(int roleId, IEnumerable<int> permissionIds)
        {
            var currentPermissions = await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .ToListAsync();

            var permissionsToRemove = currentPermissions
                .Where(cp => !permissionIds.Contains(cp.PermissionId))
                .ToList();

            _context.RolePermissions.RemoveRange(permissionsToRemove);

            var existingPermissionIds = currentPermissions.Select(cp => cp.PermissionId);
            var permissionsToAdd = permissionIds
                .Where(pid => !existingPermissionIds.Contains(pid))
                .Select(pid => new RolePermissionEntity
                {
                    RoleId = roleId,
                    PermissionId = pid
                });

            await _context.RolePermissions.AddRangeAsync(permissionsToAdd);
        }
    }
}
