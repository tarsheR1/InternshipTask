using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories
{
    public interface IUserRoleRepository
    {
        Task<UserRoleEntity> GetAsync(Guid userId, int roleId, CancellationToken cancellationToken);
        Task<List<UserRoleEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task RemoveRoleAssign(UserRoleEntity userRole, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid userId, int roleId, CancellationToken cancellationToken);
                                                                                
        Task<List<RoleEntity>> GetRolesForUserAsync(Guid userId, CancellationToken cancellationToken );
        Task<List<UserEntity>> GetUsersForRoleAsync(int roleId, CancellationToken cancellationToken);
        Task AddRoleToUserAsync(UserRoleEntity userRoleAssign, CancellationToken cancellationToken);
        Task UpdateUserRolesAsync(Guid userId, List<int> roleIds, CancellationToken cancellationToken);
    }
}