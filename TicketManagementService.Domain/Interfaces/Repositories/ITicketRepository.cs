using TicketManagementService.Domain.Entities;

namespace TicketManagementService.Domain.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket?> GetByIdAsync(string id);
        Task<IReadOnlyList<Ticket>> GetByEventIdAsync(string eventId);
        Task<IReadOnlyList<Ticket>> GetAvailableByEventIdAsync(string eventId);
        Task AddAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket);
    }
}
