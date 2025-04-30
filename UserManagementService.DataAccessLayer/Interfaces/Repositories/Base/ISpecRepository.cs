using Shared.Interfaces;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories.Base
{
    public interface ISpecRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    where TEntity : class
    {
        public Task<TEntity?> GetBySpecAsync(
            ISpecification<TEntity> spec,
            CancellationToken cancellationToken = default);

        public IQueryable<TEntity> GetAllBySpecAsync(ISpecification<TEntity> spec);

        public Task<int> CountBySpecAsync(
            ISpecification<TEntity> spec,
            CancellationToken cancellationToken = default);
    }
}
