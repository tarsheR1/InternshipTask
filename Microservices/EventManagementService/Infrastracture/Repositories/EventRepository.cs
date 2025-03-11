using EventManagementService.Domain.Interfaces;
using EventManagementService.Domain.Models;
using EventManagementService.Infrastracture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EventManagementService.Infrastracture.Repositories
{
    public class EventRepository(EventDbContext eventDbContext) : IEventRepository
    {
        private readonly EventDbContext _dbContext = eventDbContext;

        public async Task<EventEntity> GetEventAsync(Guid id)
        {
            return await _dbContext.Events
                .Include(e => e.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<EventEntity>> GetAllAsync()
        {
            return await _dbContext.Events
                .Include(e => e.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(List<EventEntity> Events, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbContext.Events
                .Include(e => e.Category); // Жадная загрузка категории

            var totalCount = await query.CountAsync();
            var events = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (events, totalCount);
        }

        public async Task UpdateAsync(EventEntity eventEntity)
        {
            _dbContext.Events.Update(eventEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(EventEntity eventEntity)
        {
            await _dbContext.Events.AddAsync(eventEntity);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(int id)
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
