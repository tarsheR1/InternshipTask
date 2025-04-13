using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<List<RoleEntity>> GetAll();

        Task<RoleEntity> GetByIdAsync(int roleId, CancellationToken cancellationToken);

        Task<RoleEntity> GetByName(string roleName, CancellationToken cancellationToken);

        Task AddAsync(RoleEntity role, CancellationToken cancellationToken);

        Task UpdateAsync(RoleEntity role, CancellationToken cancellationToken);

        Task DeleteAsync(RoleEntity role, CancellationToken cancellationToken);
    }
}
