using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories
{
    public interface IRolePermissionRepository
    {
        Task<RolePermissionEntity> GetAsync(int roleId, int permissionId);
        Task<IEnumerable<RolePermissionEntity>> GetAllAsync();
        Task AddAsync(RolePermissionEntity rolePermission);
        Task DeleteAsync(int roleId, int permissionId);
        Task<bool> ExistsAsync(int roleId, int permissionId);

        Task<IEnumerable<PermissionEntity>> GetPermissionsForRoleAsync(int roleId);
        Task<IEnumerable<RoleEntity>> GetRolesForPermissionAsync(int permissionId);
        Task AddPermissionToRoleAsync(int roleId, int permissionId);
        Task RemovePermissionFromRoleAsync(int roleId, int permissionId);
        Task UpdateRolePermissionsAsync(int roleId, IEnumerable<int> permissionIds);
    }
}
