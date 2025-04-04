using MediatR;

namespace TicketManagementService.Application.Commands
{
    public record ReserveTicketsCommand(
    Guid EventId,
    Guid UserId,
    string TicketType,
    int Quantity
    ) : IRequest<Unit>;
}
