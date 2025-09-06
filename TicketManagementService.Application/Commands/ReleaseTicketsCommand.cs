using MediatR;

namespace TicketManagementService.Application.Commands
{
    public record ReleaseTicketCommand(
    Guid TicketId
    ) : IRequest<Unit>;
}
