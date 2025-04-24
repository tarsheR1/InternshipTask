using UserManagementService.DataAccessLayer.Entities.Relations;
using UserManagementService.DataAccessLayer.Entities.Role;
using UserManagementService.DataAccessLayer.Entities.Users;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Base;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories.Relations
{
    public interface IUserRoleRepository : IManyToManyRepository<UserRoleEntity, Guid, int>
    {                                                                                 
        Task<List<RoleEntity>> GetRolesForUserAsync(Guid userId, CancellationToken cancellationToken);

        Task<List<UserEntity>> GetUsersForRoleAsync(int roleId, CancellationToken cancellationToken);
    }
}