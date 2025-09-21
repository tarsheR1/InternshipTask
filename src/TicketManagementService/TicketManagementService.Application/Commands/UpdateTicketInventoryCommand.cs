using MediatR;

namespace TicketManagementService.Application.Commands
{
    public record UpdateTicketInventoryCommand(
    Guid EventId,
    string TicketType,
    int NewTotalQuantity
    ) : IRequest<Unit>;
}