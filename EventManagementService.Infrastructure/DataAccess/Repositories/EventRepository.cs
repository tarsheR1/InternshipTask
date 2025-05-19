using EventManagementService.DataAccess.Persistence;
using EventManagementService.Domain.Entities;
using EventManagementService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventManagementService.DataAccess.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventDbContext _context;
        private readonly DbSet<EventEntity> _events;

        public EventRepository(EventDbContext context)
        {
            _context = context;
            _events = context.Set<EventEntity>();
        }

        public async Task<List<EventEntity>> GetEventsAsync(
            CancellationToken cancellationToken,
            Expression<Func<EventEntity, bool>>? filter = null,
            List<Expression<Func<EventEntity, object>>>? includes = null,
            Func<IQueryable<EventEntity>, IOrderedQueryable<EventEntity>>? orderBy = null,
            int? skip = null,
            int? take = null)
        {
            IQueryable<EventEntity> query = _events;

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                includes.ForEach(include => query = query.Include(include));

            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<EventEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => await _events
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        public async Task AddAsync(EventEntity entity, CancellationToken cancellationToken)
        {
            await _events.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(EventEntity entity, CancellationToken cancellationToken)
        {
            _events.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(EventEntity entity, CancellationToken cancellationToken)
        {
            
            _events.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            
        }
    }
}
