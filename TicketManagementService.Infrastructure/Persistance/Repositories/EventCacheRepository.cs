using MongoDB.Driver;
using TicketManagementService.Domain.Entities;
using TicketManagementService.Domain.Interfaces.Repositories;

namespace TicketManagementService.Infrastructure.Persistance.Repositories;

public class EventCacheRepository : IEventCacheRepository
{
    private readonly IMongoCollection<EventCache> _collection;

    public EventCacheRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<EventCache>("event_cache");
    }

    public async Task<EventCache?> GetByEventIdAsync(string eventId)
    {
        return await _collection
            .Find(e => e.OriginalEventId == eventId)
            .FirstOrDefaultAsync();
    }

    public async Task UpsertAsync(EventCache eventCache)
    {
        var filter = Builders<EventCache>.Filter
            .Eq(e => e.OriginalEventId, eventCache.OriginalEventId);

        await _collection.ReplaceOneAsync(
            filter,
            eventCache,
            new ReplaceOptions { IsUpsert = true });
    }

    public async Task DeleteAsync(string eventId)
    {
        await _collection.DeleteOneAsync(e => e.OriginalEventId == eventId);
    }

    public async Task<IReadOnlyList<EventCache>> GetUpcomingEventsAsync(DateTime fromDate)
    {
        return await _collection
            .Find(e => e.Date >= fromDate)
            .SortBy(e => e.Date)
            .ToListAsync();
    }
}