using Shared.Interfaces;
using Shared.Pagination;
using UserManagementService.DataAccessLayer.Entities.Users;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Base;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories.Users
{
    public interface IUserRepository : IPagedRepository <UserEntity, Guid>
    {
        Task<UserEntity> GetByEmailAsync(
            string email, 
            CancellationToken cancellationToken);

        Task<List<string>> GetUserRolesAsync(
            Guid userId,
            CancellationToken cancellationToken);

        public Task<UserEntity> GetBySpecAsync(
            ISpecification<UserEntity> spec,
            CancellationToken cancellationToken = default);

        public Task<(List<UserEntity> Items, int TotalCount)> GetAllBySpecAsync(
            ISpecification<UserEntity> spec,
            PaginationParameters paginationParameters,
            CancellationToken cancellationToken = default);

        public Task<int> CountBySpecAsync(
            ISpecification<UserEntity> spec,
            CancellationToken cancellationToken = default);

        public Task<bool> AnyBySpecAsync(
            ISpecification<UserEntity> spec,
            CancellationToken cancellationToken = default);

    }
}
    