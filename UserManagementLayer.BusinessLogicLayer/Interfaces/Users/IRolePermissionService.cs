using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Users
{
    public interface IRolePermissionService
    {
        // Role Management
        Task<Role> CreateRoleAsync(RoleCreateCommand command, CancellationToken cancellationToken);
        Task<Role> UpdateRoleAsync(Guid roleId, RoleUpdateCommand command, CancellationToken cancellationToken);
        Task DeleteRoleAsync(Guid roleId, CancellationToken cancellationToken);
        Task<Role> GetRoleByIdAsync(Guid roleId, CancellationToken cancellationToken);
        Task<List<Role>> GetAllRolesAsync(CancellationToken cancellationToken);

        // Permission Management
        Task<List<Permission>> GetAllPermissionsAsync(CancellationToken cancellationToken);

        // Role-Permission Assignment
        Task AssignPermissionToRoleAsync(Guid roleId, Guid permissionId, CancellationToken cancellationToken);
        Task RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId, CancellationToken cancellationToken);
        Task<List<Permission>> GetRolePermissionsAsync(Guid roleId, CancellationToken cancellationToken);
    }
}
