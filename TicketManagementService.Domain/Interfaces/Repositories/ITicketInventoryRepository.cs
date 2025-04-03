using TicketManagementService.Domain.Aggregates;

namespace TicketManagementService.Domain.Interfaces.Repositories
{
    public interface ITicketInventoryRepository
    {
        Task<TicketInventory> GetByEventAndTypeAsync(Guid eventId, string ticketType, CancellationToken cancellationToken);
        Task UpdateInventoryAsync(TicketInventory inventory, CancellationToken cancellationToken);
    }
}
