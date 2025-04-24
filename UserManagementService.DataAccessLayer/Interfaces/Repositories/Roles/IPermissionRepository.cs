using UserManagementService.DataAccessLayer.Entities.Role;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories.Roles
{
    public interface IPermissionRepository
    {
        Task<List<PermissionEntity>> GetAll(CancellationToken cancellationToken);

        Task<PermissionEntity> GetByIdAsync(int permissionId, CancellationToken cancellationToken);

        Task<PermissionEntity> GetByName(string permissionName, CancellationToken cancellationToken);
    }
}