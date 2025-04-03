using MediatR;

namespace TicketManagementService.Application.Commands
{
    public record ReserveTicketsCommand(
    Guid EventId,
    string TicketType,
    int Quantity
    ) : IRequest<Unit>;
}
