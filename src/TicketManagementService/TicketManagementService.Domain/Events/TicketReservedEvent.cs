using TicketManagementService.Domain.Common;

namespace TicketManagementService.Domain.Events
{
    public class TicketReservedEvent : DomainEvent
    {
        public Guid TicketId { get; }
        public Guid UserId { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;  

        public TicketReservedEvent(Guid ticketId, Guid userId)
        {
            TicketId = ticketId;
            UserId = userId;
        }
    }
}
