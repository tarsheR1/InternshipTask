using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.DataAccessLayer.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<UserRoleEntity> GetAsync(Guid userId, int roleId, CancellationToken cancellationToken);
        Task<IEnumerable<UserRoleEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(UserRoleEntity userRole, CancellationToken cancellationToken);
        Task DeleteAsync(Guid userId, int roleId, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid userId, int roleId, CancellationToken cancellationToken);
                                                                                
        Task<IEnumerable<RoleEntity>> GetRolesForUserAsync(Guid userId, CancellationToken cancellationToken );
        Task<IEnumerable<UserEntity>> GetUsersForRoleAsync(int roleId, CancellationToken cancellationToken);
        Task AddRoleToUserAsync(Guid userId, int roleId, CancellationToken cancellationToken);
        Task RemoveRoleFromUserAsync(Guid userId, int roleId, CancellationToken cancellationToken);
        Task UpdateUserRolesAsync(Guid userId, IEnumerable<int> roleIds, CancellationToken cancellationToken);
    }
}