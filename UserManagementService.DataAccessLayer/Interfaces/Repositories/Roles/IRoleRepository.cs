using UserManagementService.DataAccessLayer.Entities.Role;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Base;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories.Roles
{
    public interface IRoleRepository : INameSearchableRepository<RoleEntity, int>
    {
        Task<List<RoleEntity>> GetAllAsync(CancellationToken cancellationToken);
    }
}
