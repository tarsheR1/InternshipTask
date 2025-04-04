using TicketManagementService.Domain.Aggregates;

namespace TicketManagementService.Domain.Interfaces.Repositories
{
    public interface ITicketInventoryWriteRepository
    {
        Task AddAsync(TicketInventory inventory);
        Task UpdateAsync(TicketInventory inventory);
        Task DeleteAsync(Guid id);
    }
}
