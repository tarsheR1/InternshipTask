using Shared.Pagination;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories.Base
{
    public interface IPagedRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    where TEntity : class
    {
        Task<(List<TEntity> Items, int TotalCount)> GetPagedAsync(
            PaginationParameters parameters,
            CancellationToken cancellationToken);
    }
}
