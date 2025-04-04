using TicketManagementService.Domain.Aggregates;

namespace TicketManagementService.Domain.Interfaces.Repositories
{
    public interface ITicketInventoryReadRepository
    {
        Task<TicketInventory> GetAvailabilityAsync(Guid eventId, string ticketType);
        Task<IReadOnlyList<TicketInventory>> GetTicketInventoriesByEventId(Guid eventId);
    }
}
