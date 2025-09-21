using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementService.Domain.Aggregates;
using TicketManagementService.Domain.Exceptions;
using TicketManagementService.Domain.Interfaces.Repositories.Write;

namespace TicketManagementService.Infrastructure.Persistance.Repositories.Write
{
    public class TicketInventoryWriteRepository : ITicketInventoryWriteRepository
    {
        private readonly IMongoCollection<TicketInventory> _collection;

        public TicketInventoryWriteRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<TicketInventory>("TicketInventories");
        }

        public async Task AddAsync(TicketInventory inventory)
        {
            await _collection.InsertOneAsync(inventory);
        }

        public async Task UpdateAsync(TicketInventory inventory)
        {
            var filter = Builders<TicketInventory>.Filter.And(
                Builders<TicketInventory>.Filter.Eq(x => x.EventId, inventory.EventId),
                Builders<TicketInventory>.Filter.Eq(x => x.TicketType, inventory.TicketType)
            );

            await _collection.ReplaceOneAsync(filter, inventory);
        }

        public async Task DeleteAsync(Guid eventId, string ticketType)
        {
            var filter = Builders<TicketInventory>.Filter.And(
                Builders<TicketInventory>.Filter.Eq(x => x.EventId, eventId),
                Builders<TicketInventory>.Filter.Eq(x => x.TicketType, ticketType)
            );

            await _collection.DeleteOneAsync(filter);
        }

        public async Task ReserveTickets(Guid eventId, string ticketType, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive");

            // Сначала проверяем существование инвентаря
            var inventoryExists = await _collection.Find(
                x => x.EventId == eventId && x.TicketType == ticketType)
                .AnyAsync();

            if (!inventoryExists)
                throw new TicketInventoryNotFoundException(ticketType);

            // Затем пытаемся выполнить атомарное обновление
            var filter = Builders<TicketInventory>.Filter.And(
                Builders<TicketInventory>.Filter.Eq(x => x.EventId, eventId),
                Builders<TicketInventory>.Filter.Eq(x => x.TicketType, ticketType),
                Builders<TicketInventory>.Filter.Gte(x => x.AvailableQuantity, quantity)
            );

            var update = Builders<TicketInventory>.Update
                .Inc(x => x.AvailableQuantity, -quantity)
                .Inc(x => x.ReservedQuantity, quantity);

            var result = await _collection.FindOneAndUpdateAsync(
                filter,
                update,
                new FindOneAndUpdateOptions<TicketInventory>
                {
                    ReturnDocument = ReturnDocument.After
                });

            if (result == null)
            {
                // Получаем текущее количество для информативного сообщения
                var currentInventory = await _collection.Find(
                    x => x.EventId == eventId && x.TicketType == ticketType)
                    .FirstOrDefaultAsync();

                throw new TicketsSoldOutException(ticketType, quantity);
            }
        }
    }
}
