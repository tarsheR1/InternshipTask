namespace UserManagementService.DataAccessLayer.Interfaces.Repositories.Base
{
    public interface INameSearchableRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    where TEntity : class
    {
        Task<TEntity> GetByNameAsync(string name, CancellationToken cancellationToken);
    }
}
