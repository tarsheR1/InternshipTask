using TicketManagementService.Domain.Common;
using TicketManagementService.Domain.Events;
using TicketManagementService.Domain.Exceptions;

namespace TicketManagementService.Domain.Aggregates
{
    public class TicketInventory 
    {
        private readonly List<DomainEvent> _domainEvents = new();

        public Guid EventId { get; private set; }
        public string TicketType { get; private set; }
        public int TotalQuantity { get; private set; }
        public int AvailableQuantity { get; private set; }
        public int ReservedQuantity { get; private set; }

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public void ClearDomainEvents() => _domainEvents.Clear();


        private TicketInventory() { }

        public TicketInventory(Guid eventId, string ticketType, int totalQuantity)
        {
            EventId = eventId;
            TicketType = ticketType;
            TotalQuantity = totalQuantity;
            AvailableQuantity = totalQuantity;
            ReservedQuantity = 0;
        }

        public void ReserveTickets(int quantity, Guid userId)
        {
            if (AvailableQuantity < quantity)
                throw new TicketsSoldOutException(TicketType);

            AvailableQuantity -= quantity;
            ReservedQuantity += quantity;

            _domainEvents.Add(new TicketReservedEvent(
                ticketId: Guid.NewGuid(), 
                userId: userId
            ));
        }

        public void ReleaseTickets(int quantity)
        {
            if (ReservedQuantity < quantity)
                throw new InvalidTicketOperationException();

            ReservedQuantity -= quantity;
            AvailableQuantity += quantity;
        }

        public void UpdateTotalQuantity(int newTotal)
        {
            if (newTotal < (TotalQuantity - AvailableQuantity))
                throw new TicketsSoldOutException("");

            TotalQuantity = newTotal;
            AvailableQuantity = newTotal - ReservedQuantity;
        }

        public void UpdateTicketType(string newTicketType)
        {
            TicketType = newTicketType;
        }

    }
}
