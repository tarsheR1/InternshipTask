using Shared.Pagination;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request.Users;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.BusinessLogicLayer.Models.Queries;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Users
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<PagedResponse<User>> GetUsersPaginatedAsync(
            PaginationParameters paginationParameters,
            //UserFilter filter = null,
            SortOptions sort = null,
            CancellationToken cancellationToken = default);

        Task<User> CreateUserAsync(CreateUserRequestDto requestDto, CancellationToken cancellationToken);
        Task UpdateUserAsync(Guid userId, UpdateUserRequestDto updateRequest, CancellationToken cancellationToken);
        Task ActivateUserAsync(Guid userId, CancellationToken cancellationToken);
        Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken);
    }
}
