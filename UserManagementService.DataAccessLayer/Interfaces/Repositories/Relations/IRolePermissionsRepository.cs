using UserManagementService.DataAccessLayer.Entities.Relations;
using UserManagementService.DataAccessLayer.Entities.Role;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Base;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories.Relations
{
    public interface IRolePermissionRepository : IManyToManyRepository<RolePermissionEntity, int, int>
    {
        Task<List<PermissionEntity>> GetPermissionsForRoleAsync(int roleId, CancellationToken cancellationToken = default);
        Task<List<RoleEntity>> GetRolesForPermissionAsync(int permissionId, CancellationToken cancellationToken = default);
    }
}
