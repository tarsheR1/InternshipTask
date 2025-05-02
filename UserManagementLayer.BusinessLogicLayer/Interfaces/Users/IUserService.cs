using Shared.Pagination;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.BusinessLogicLayer.Models.Queries;
using UserManagementService.DataAccessLayer.Specifications.Users;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Users
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);

        public Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

        Task<PagedResponse<User>> GetUsersPaginatedAsync(
            PaginationParameters paginationParameters,
            UserFilter filter = null,
            SortOptions sort = null,
            CancellationToken cancellationToken = default);

        Task UpdateUserAsync(
            Guid userId, 
            UpdateUserRequest updateRequest, 
            CancellationToken cancellationToken);  

        Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken);

        
    }
}
