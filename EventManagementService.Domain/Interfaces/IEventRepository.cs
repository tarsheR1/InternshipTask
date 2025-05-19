using EventManagementService.Domain.Entities;
using System.Linq.Expressions;

namespace EventManagementService.Domain.Interfaces
{
    public interface IEventRepository
    {
        Task AddAsync(EventEntity entity, CancellationToken cancellationToken);
        
        Task DeleteAsync(EventEntity entity, CancellationToken cancellationToken);

        Task<EventEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<List<EventEntity>> GetEventsAsync(

            CancellationToken cancellationToken = default,
            Expression<Func<EventEntity, bool>>? filter = null, 
            List<Expression<Func<EventEntity, object>>>? includes = null, 
            Func<IQueryable<EventEntity>, 
            IOrderedQueryable<EventEntity>>? orderBy = null, 
            int? skip = null, 
            int? take = null);

        Task UpdateAsync(EventEntity entity, CancellationToken cancellationToken);
    }
}