using TicketManagementService.Domain.Common;
using TicketManagementService.Domain.Events;
using TicketManagementService.Domain.Exceptions;

namespace TicketManagementService.Domain.Aggregates
{
    public class TicketInventory 
    {
        private readonly List<DomainEvent> _domainEvents = new();

        public Guid EventId { get; init; }
        public string TicketType { get; init; }
        public int TotalQuantity { get; init; }
        public int AvailableQuantity { get; init; }
        public int ReservedQuantity { get; init; }

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public void ClearDomainEvents() => _domainEvents.Clear();


        public TicketInventory() { }

        public TicketInventory(Guid eventId, string ticketType, int totalQuantity)
        {
            EventId = eventId;
            TicketType = ticketType;
            TotalQuantity = totalQuantity;
            AvailableQuantity = totalQuantity;
            ReservedQuantity = 0;
        }

        public void ReserveTicket(Guid ticketId, Guid userId)
        {
            if (AvailableQuantity <  1)
                throw new InvalidTicketOperationException();

            AvailableQuantity++;
            ReservedQuantity--;

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
                throw new TicketsSoldOutException(TicketType, AvailableQuantity);

            TotalQuantity = newTotal;
            AvailableQuantity = newTotal - ReservedQuantity;
        }

        public void UpdateTicketType(string newTicketType)
        {
            TicketType = newTicketType;
        }

    }
}
