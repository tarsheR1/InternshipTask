namespace UserManagementService.DataAccessLayer.Interfaces.Repositories.Base
{
    public interface IManyToManyRepository<TEntity, TFirstKey, TSecondKey>
    where TEntity : class
    {
        Task<TEntity> GetAsync(TFirstKey firstId, TSecondKey secondId, CancellationToken cancellationToken);

        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
        
        Task<bool> ExistsAsync(TFirstKey firstId, TSecondKey secondId, CancellationToken cancellationToken);
    }
}
