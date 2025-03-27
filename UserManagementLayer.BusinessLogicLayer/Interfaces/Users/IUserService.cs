using UserManagementService.BusinessLogicLayer.Models;
using UserManagementService.BusinessLogicLayer.Models.Commands;

namespace UserManagementService.BusinessLogicLayer.Services.Interfaces
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
