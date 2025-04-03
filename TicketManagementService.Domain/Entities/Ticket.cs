using TicketManagementService.Domain.Enums;
using TicketManagementService.Domain.Common;
using TicketManagementService.Domain.Events;
using TicketManagementService.Domain.Exceptions;

namespace TicketManagementService.Domain.Entities
{
    public class Ticket : BaseEntity<Guid>
    {
        public Guid EventId { get; private set; }
        public Guid UserId { get; private set; }
        public string TicketType { get; private set; }
        public TicketStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ExpiresAt { get; private set; }

        private Ticket() { }

        public Ticket(
            Guid eventId,
            Guid userId,
            string ticketType,
            DateTime createdAt,
            DateTime? expiresAt)
        {
            Id = Guid.NewGuid();
            EventId = eventId;
            UserId = userId;
            TicketType = ticketType;
            Status = TicketStatus.Reserved;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;

            AddDomainEvent(new TicketReservedEvent(Id, userId));
        }

        public void ConfirmPayment()
        {
            if (Status != TicketStatus.Reserved)
                throw new InvalidTicketStatusException(Status);

            Status = TicketStatus.Paid;
            ExpiresAt = null; // Отменяем таймер резервации
        }

        public void Cancel()
        {
            if (Status == TicketStatus.Cancelled)
                throw new InvalidTicketStatusException(Status);

            Status = TicketStatus.Cancelled;
            AddDomainEvent(new TicketCancelledEvent(Id, EventId, TicketType));
        }
    }
}
