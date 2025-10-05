using UserManagementService.BusinessLogicLayer.Commands;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Users
{
    public interface IUserService
    {
        Task<bool> IsEmailAvailableAsync(string email, CancellationToken cancellationToken = default);
        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<User> CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken = default);

        Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
               
        Task UpdateUserAsync(
            Guid userId, 
            UserUpdateCommand updateRequest, 
            CancellationToken cancellationToken);
        
        Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
