using MediatR;
using TicketManagementService.Application.DTO;

namespace TicketManagementService.Application.Commands
{
    public record CreateTicketTypeCommand : IRequest<CreateTicketTypeResult>
    {
        public Guid EventId { get; init; }
        public string TicketType { get; init; }
        public int TotalQuantity { get; init; }
    }
}
