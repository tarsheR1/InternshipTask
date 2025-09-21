using EventManagementService.Domain.Entities;
using EventManagementService.Domain.Interfaces.Specification;
using EventManagementService.Domain.Pagination;

namespace EventManagementService.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        public Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
        public Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        public Task<IReadOnlyCollection<TEntity>> GetListBySpecAsync(
            ISpecification<TEntity> spec, 
            PaginationOptions options,
            CancellationToken cancellationToken = default);
        public Task<TEntity?> GetBySpecAsync(
            ISpecification<TEntity> spec,
            CancellationToken cancellationToken = default);
        public Task<int> СountBySpecAsync(
            ISpecification<TEntity> spec,
            CancellationToken cancellationToken = default);

        public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default);

        protected IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec);
    }
}
