using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Users
{
    public interface IPermissionService
    {
        Task<List<Permission>> GetAllPermissionsAsync(CancellationToken cancellationToken);

        Task<Permission> GetPermissionByIdAsync(int permissionId, CancellationToken cancellationToken);
    }
}
