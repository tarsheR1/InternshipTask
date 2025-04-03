using MediatR;

namespace TicketManagementService.Application.Commands
{
    public record ReleaseTicketsCommand(
    Guid EventId,
    string TicketType,
    int Quantity
    ) : IRequest<Unit>;
}
