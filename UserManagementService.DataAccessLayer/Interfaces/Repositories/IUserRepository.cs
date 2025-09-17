using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> GetByIdAsync(Guid userId, CancellationToken cancellationToken);

        Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken);

        Task AddAsync(UserEntity user, CancellationToken cancellationToken);

        Task UpdateAsync(UserEntity user, CancellationToken cancellationToken);

        Task DeleteAsync(UserEntity user, CancellationToken cancellationToken);

        Task<List<string>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken);
    }
}
