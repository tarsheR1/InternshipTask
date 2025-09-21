using TicketManagementService.Domain.Entities;

namespace TicketManagementService.Domain.Interfaces.Repositories.Read
{
    public interface ITicketReadRepository
    {
        Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IReadOnlyList<Ticket>> GetByEventIdAsync(Guid eventId, CancellationToken cancellationToken);

        Task<IReadOnlyList<Ticket>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

        Task<int> CountReservedTicketsAsync(Guid eventId, CancellationToken cancellationToken);
    }
}
