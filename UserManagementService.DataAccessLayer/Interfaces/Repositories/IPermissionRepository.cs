using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories
{
    public interface IPermissionRepository
    {
        Task<List<PermissionEntity>> GetAll(CancellationToken cancellationToken);

        Task<PermissionEntity> GetByIdAsync(int permissionId, CancellationToken cancellationToken);

        Task<PermissionEntity> GetByName(string permissionName, CancellationToken cancellationToken);
    }
}