using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories
{
    public interface IRolePermissionRepository
    {
        Task<RolePermissionEntity> GetAsync(int roleId, int permissionId, CancellationToken cancellationToken = default);
        Task<IEnumerable<RolePermissionEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(RolePermissionEntity rolePermission, CancellationToken cancellationToken = default);
        Task DeleteAsync(int roleId, int permissionId, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int roleId, int permissionId, CancellationToken cancellationToken = default);

        Task<IEnumerable<PermissionEntity>> GetPermissionsForRoleAsync(int roleId, CancellationToken cancellationToken = default);
        Task<IEnumerable<RoleEntity>> GetRolesForPermissionAsync(int permissionId, CancellationToken cancellationToken = default);
        Task AddPermissionToRoleAsync(int roleId, int permissionId, CancellationToken cancellationToken = default);
        Task RemovePermissionFromRoleAsync(int roleId, int permissionId, CancellationToken cancellationToken = default);
        Task UpdateRolePermissionsAsync(int roleId, IEnumerable<int> permissionIds, CancellationToken cancellationToken = default);
    }
}
