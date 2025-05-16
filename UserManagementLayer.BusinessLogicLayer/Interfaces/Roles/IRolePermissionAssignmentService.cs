using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Users
{
    public interface IRolePermissionAssignmentService
    {
        Task AssignPermissionToRoleAsync(int roleId, int permissionId, CancellationToken cancellationToken );

        Task RemovePermissionFromRoleAsync(int roleId, int permissionId, CancellationToken cancellationToken);

        Task<List<Permission>> GetRolePermissionsAsync(int roleId, CancellationToken cancellationToken);
    }
}
