using UserManagementService.DataAccessLayer.Entities.Role;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Base;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories.Roles
{
    public interface IRoleRepository : IBaseRepository <RoleEntity, int>
    {
        Task<List<RoleEntity>> GetAllAsync(CancellationToken cancellationToken);

        Task<RoleEntity> GetByNameAsync(string name, CancellationToken cancellationToken);
    }
}
