using TicketManagementService.Domain.Common;

namespace TicketManagementService.Domain.Events
{
    class TicketCancelledEvent : DomainEvent
    {
        public Guid TicketId { get; }
        public Guid EventId { get; }
        public string TicketType { get; }
        public DateTime OccurredOn { get; }  = DateTime.UtcNow; 

        public TicketCancelledEvent(Guid ticketId, Guid eventId, string ticketType)
        {
            TicketId = ticketId;
            EventId = eventId;
            TicketType = ticketType;
        }
    }
}
