namespace UserManagementService.DataAccessLayer.Interfaces.Repositories.Base
{
    public interface IPagedRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    where TEntity : class
    {
        Task<(List<TEntity> Items, int TotalCount)> GetPagedAsync(
            int skip,
            int take,
            CancellationToken cancellationToken);
    }
}
