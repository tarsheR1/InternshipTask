using TicketManagementService.Domain.Aggregates;

namespace TicketManagementService.Domain.Interfaces.Repositories.Write
{
    public interface ITicketInventoryWriteRepository
    {
        Task AddAsync(TicketInventory inventory);

        Task UpdateAsync(TicketInventory inventory);

        Task DeleteAsync(Guid id, string ticketType);

        Task ReserveTickets(
            Guid eventId,
            string ticketType,
            int quantity);
    }
}
