namespace UserManagementService.DataAccessLayer.Interfaces.Repositories.Base
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    }
}
