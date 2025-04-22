using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.BusinessLogicLayer.Models.Pagination;

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

        Task<PagedResponse<User>> GetUsersPaginatedAsync(
            PaginationParameters paginationParameters,
            CancellationToken cancellationToken);
    }
}
