using MongoDB.Driver;
using TicketManagementService.Domain.Enums;
using TicketManagementService.Domain.Entities;
using TicketManagementService.Domain.Interfaces.Repositories.Read;


namespace TicketManagementService.Infrastructure.Persistance.Repositories.Read
{
    public class TicketReadRepository : ITicketReadRepository
    {
        private readonly IMongoCollection<Ticket> _collection;

        public TicketReadRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Ticket>("tickets");
        }

        public async Task<Ticket?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _collection.Find(x => x.Id == id)
                                 .FirstOrDefaultAsync(ct);
        }

        public async Task<IReadOnlyList<Ticket>> GetByEventIdAsync(Guid eventId, CancellationToken ct = default)
        {
            return await _collection.Find(x => x.EventId == eventId)
                                 .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Ticket>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
        {
            return await _collection.Find(x => x.UserId == userId)
                                 .ToListAsync(ct);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            return await _collection.Find(x => x.Id == id)
                                 .AnyAsync(ct);
        }

        public async Task<int> CountReservedTicketsAsync(Guid eventId, CancellationToken ct = default)
        {
            return (int)await _collection.CountDocumentsAsync(
                x => x.EventId == eventId && x.Status == TicketStatus.Reserved,
                cancellationToken: ct);
        }
    }
}
