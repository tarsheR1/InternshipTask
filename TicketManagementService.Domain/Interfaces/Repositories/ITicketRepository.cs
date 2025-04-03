using TicketManagementService.Domain.Entities;

namespace TicketManagementService.Domain.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket> GetByIdAsync(Guid ticketId, CancellationToken cancellationToken);
        Task<IEnumerable<Ticket>> GetByEventAsync(Guid eventId, CancellationToken cancellationToken);
        Task<IEnumerable<Ticket>> GetByUser(Guid userId, CancellationToken cancellationToken);
        Task AddAsync(Ticket ticket, CancellationToken cancellationToken);
        Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken);
    }
}
