using UserManagementService.DataAccessLayer.Entities.Users;
using UserManagementService.DataAccessLayer.Specifications.Base;

namespace UserManagementService.DataAccessLayer.Interfaces.Repositories.Base
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken);

        public Task<IReadOnlyList<TDestination>> GetAllAsync<TDestination>(
        Specification<UserEntity> specification,
        CancellationToken cancellationToken = default);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
