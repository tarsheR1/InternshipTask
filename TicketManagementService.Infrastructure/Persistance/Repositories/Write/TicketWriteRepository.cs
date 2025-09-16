using MongoDB.Driver;
using TicketManagementService.Domain.Entities;
using TicketManagementService.Domain.Enums;
using TicketManagementService.Domain.Interfaces.Repositories.Write;

namespace TicketManagementService.Infrastructure.Persistance.Repositories.Write
{
    public class TicketWriteRepository : ITicketWriteRepository
    {
        private readonly IMongoCollection<Ticket> _collection;

        public TicketWriteRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Ticket>("tickets");
            EnsureIndexes();
        }

        private void EnsureIndexes()
        {
            _collection.Indexes.CreateOne(
                new CreateIndexModel<Ticket>(
                    Builders<Ticket>.IndexKeys
                        .Ascending(x => x.Id)
                        .Ascending(x => x.Status),
                new CreateIndexOptions { Name = "IX_StatusUpdates" }));
        }

        public async Task AddAsync(Ticket ticket, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(ticket, cancellationToken);
        }

        public async Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken)
        {
            await _collection.ReplaceOneAsync(
                x => x.Id == ticket.Id,
                ticket,
                new ReplaceOptions { IsUpsert = false },
                cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> ConfirmPaymentAsync(Guid ticketId, CancellationToken ct = default)
        {
            var filter = Builders<Ticket>.Filter.And(
                Builders<Ticket>.Filter.Eq(x => x.Id, ticketId),
                Builders<Ticket>.Filter.Eq(x => x.Status, TicketStatus.Reserved)
            );

            var update = Builders<Ticket>.Update
                .Set(x => x.Status, TicketStatus.Paid)
                .Set(x => x.ExpiresAt, null);

            var result = await _collection.UpdateOneAsync(filter, update, cancellationToken: ct);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> CancelAsync(Guid ticketId, CancellationToken ct = default)
        {
            var filter = Builders<Ticket>.Filter.And(
                Builders<Ticket>.Filter.Eq(x => x.Id, ticketId),
                Builders<Ticket>.Filter.Ne(x => x.Status, TicketStatus.Cancelled)
            );

            var update = Builders<Ticket>.Update
                .Set(x => x.Status, TicketStatus.Cancelled);

            var result = await _collection.UpdateOneAsync(filter, update, cancellationToken: ct);
            return result.ModifiedCount > 0;
        }

        public async Task<int> BulkConfirmPaymentsAsync(IEnumerable<Guid> ticketIds, CancellationToken cancellationToken)
        {
            var filter = Builders<Ticket>.Filter.And(
                Builders<Ticket>.Filter.In(x => x.Id, ticketIds),
                Builders<Ticket>.Filter.Eq(x => x.Status, TicketStatus.Reserved)
            );

            var update = Builders<Ticket>.Update
                .Set(x => x.Status, TicketStatus.Paid)
                .Set(x => x.ExpiresAt, null);

            var result = await _collection.UpdateManyAsync(
                filter, 
                update,
                cancellationToken: cancellationToken);

            return (int)result.ModifiedCount;
        }
    }
}
