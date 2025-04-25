using EventManagementService.Domain.Models;
using System.Linq.Expressions;

namespace EventManagementService.DataAccess.Repositories
{
    public interface IEventRepository
    {
        Task AddAsync(EventEntity entity, CancellationToken cancellationToken);
        
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<EventEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<List<EventEntity>> GetEventsAsync(
            Expression<Func<EventEntity, bool>>? filter = null, 
            List<Expression<Func<EventEntity, object>>>? includes = null, 
            Func<IQueryable<EventEntity>, 
            IOrderedQueryable<EventEntity>>? orderBy = null, 
            int? skip = null, 
            int? take = null, 
            CancellationToken cancellationToken = default);

        Task UpdateAsync(EventEntity entity, CancellationToken cancellationToken);
    }
}