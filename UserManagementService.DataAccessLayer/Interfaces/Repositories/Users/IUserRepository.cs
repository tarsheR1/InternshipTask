using UserManagementService.DataAccessLayer.Entities.Users;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Base;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories.Users
{
    public interface IUserRepository : ISpecRepository <UserEntity, Guid>
    {
        Task<UserEntity> GetByEmailAsync(
            string email, 
            CancellationToken cancellationToken);

        Task<List<string>> GetUserRolesAsync(
            Guid userId,
            CancellationToken cancellationToken);
    }
}
    