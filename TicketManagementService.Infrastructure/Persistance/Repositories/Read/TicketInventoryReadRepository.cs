using MongoDB.Driver;
using TicketManagementService.Domain.Aggregates;
using TicketManagementService.Domain.Interfaces.Repositories.Read;

namespace TicketManagementService.Infrastructure.Persistance.Repositories.Read
{
    class TicketInventoryReadRepository : ITicketInventoryReadRepository
    {
        private readonly IMongoCollection<TicketInventory> _collection;

        public TicketInventoryReadRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<TicketInventory>("ticket_inventories");
        }

        public async Task<TicketInventory> GetAvailabilityAsync(Guid eventId, string ticketType)
        {
            return await _collection.Find(x => x.EventId == eventId && x.TicketType == ticketType)
                                  .FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<TicketInventory>> GetTicketInventoriesByEventId(Guid eventId)
        {
            var inventories = await _collection.Find(x => x.EventId == eventId)
                                             .ToListAsync();
            return inventories.AsReadOnly();
        }

        public async Task<bool> ExistsAsync(Guid eventId, string ticketType)
        {
            return await _collection.Find(x => x.EventId == eventId && x.TicketType == ticketType)
                                   .AnyAsync();
        }
    }
}
