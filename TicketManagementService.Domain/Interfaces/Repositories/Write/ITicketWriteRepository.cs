using TicketManagementService.Domain.Entities;

namespace TicketManagementService.Domain.Interfaces.Repositories.Write
{
    public interface ITicketWriteRepository
    {
        Task AddAsync(Ticket ticket, CancellationToken cancellationToken);

        Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);


        Task<bool> ConfirmPaymentAsync(Guid ticketId, CancellationToken cancellationToken);

        Task<bool> CancelAsync(Guid ticketId, CancellationToken cancellationToken);

        Task<int> BulkConfirmPaymentsAsync(IEnumerable<Guid> ticketIds, CancellationToken cancellationToken);
    }
}
