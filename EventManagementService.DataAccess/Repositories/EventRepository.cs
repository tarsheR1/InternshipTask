using EventManagementService.DataAccess.Persistence;
using EventManagementService.Domain.Interfaces;
using EventManagementService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementService.DataAccess.Repositories
{
    public class EventRepository(EventDbContext eventDbContext) : IEventRepository
    {
        private readonly EventDbContext _dbContext = eventDbContext;

        public async Task<EventEntity> GetEventAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Events
                .Include(e => e.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<List<EventEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Events
                .Include(e => e.Category)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<(List<EventEntity> Events, int TotalCount)> GetPagedAsync(
            int skip, 
            int take, 
            CancellationToken cancellationToken)
        {
            var query = _dbContext.Events
                .Include(e => e.Category); 

            var totalCount = await query.CountAsync(cancellationToken);
            var events = await query
                .Skip((skip - 1) * take)
                .Take(take)
                .ToListAsync(cancellationToken);

            return (events, totalCount);
        }

        public async Task UpdateAsync(EventEntity eventEntity, CancellationToken cancellationToken)
        {
            _dbContext.Events.Update(eventEntity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddAsync(EventEntity eventEntity, CancellationToken cancellationToken)
        {
            await _dbContext.Events.AddAsync(eventEntity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(EventEntity eventEntity, CancellationToken cancellationToken)
        {
            _dbContext.Events.Remove(eventEntity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
