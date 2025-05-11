using UserManagementService.DataAccessLayer.Entities.Role;
using UserManagementService.DataAccessLayer.Entities.Users;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Base;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories.Users
{
    public interface IUserRepository : IBaseRepository<UserEntity, Guid>
    {
        Task<UserEntity> GetByEmailAsync(
            string email, 
            CancellationToken cancellationToken);

        Task<List<RoleEntity>> GetUserRolesAsync(
            Guid userId,
            CancellationToken cancellationToken);
    }
}
    