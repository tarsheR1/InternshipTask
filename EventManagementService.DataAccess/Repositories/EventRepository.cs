using EventManagementService.DataAccess.Persistence;
using EventManagementService.Domain.Interfaces;
using EventManagementService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementService.DataAccess.Repositories
{
    public class EventRepository(EventDbContext eventDbContext) : IEventRepository
    {
        private readonly EventDbContext _dbContext = eventDbContext;

        public async Task<EventEntity> GetEventAsync(Guid id, CancellationToken cancellation)
        {
            return await _dbContext.Events
                .Include(e => e.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<EventEntity>> GetAllAsync(CancellationToken cancellation)
        {
            return await _dbContext.Events
                .Include(e => e.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(List<EventEntity> Events, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellation)
        {
            var query = _dbContext.Events
                .Include(e => e.Category); 

            var totalCount = await query.CountAsync();
            var events = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (events, totalCount);
        }

        public async Task UpdateAsync(EventEntity eventEntity, CancellationToken cancellation)
        {
            _dbContext.Events.Update(eventEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(EventEntity eventEntity, CancellationToken cancellation)
        {
            await _dbContext.Events.AddAsync(eventEntity);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            var entity = await _dbContext.Events.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Events.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
