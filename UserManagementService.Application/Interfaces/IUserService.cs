using UserManagementService.Core.Models;

namespace UserManagementService.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid userId);
       
        Task<User> CreateUserAsync(string email, string passwordHash, string firstName, string lastName, string middleName, string phone);
        
        Task UpdateUserAsync(Guid userId, string email, string firstName, string lastName, string middleName, string phone);
        
        Task DeleteUserAsync(Guid userId);
    }
}
