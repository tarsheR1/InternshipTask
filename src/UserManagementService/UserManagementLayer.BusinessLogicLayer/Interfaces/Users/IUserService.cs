using UserManagementService.BusinessLogicLayer.Commands;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Users
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
               
        Task UpdateUserAsync(
            Guid userId, 
            UserUpdateCommand updateRequest, 
            CancellationToken cancellationToken);
        
        Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken);
    }
}
