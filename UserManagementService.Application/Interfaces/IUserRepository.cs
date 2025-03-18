using UserManagementService.Domain.Models;

namespace UserManagementService.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid userId);

        Task<User> GetByEmailAsync(string email);

        Task AddAsync(User user);

        Task UpdateAsync(User user);

        Task DeleteAsync(User user);

        Task<List<string>> GetUserRolesAsync(Guid userId);
    }
}
