using MediatR;
using TicketManagementService.Domain.Aggregates;

namespace TicketManagementService.Application.Queries
{
    public record GetTicketInventoriesForEventQuery(
        Guid eventId
    ) : IRequest<IReadOnlyList<TicketInventory>>;
}
