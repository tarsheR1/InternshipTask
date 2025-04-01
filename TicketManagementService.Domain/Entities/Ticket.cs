using TicketManagementService.Domain.Common;
using TicketManagementService.Domain.Enums;
using TicketManagementService.Domain.Exceptions;

namespace TicketManagementService.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public string EventId { get; }        
        public string Type { get; }             
        public string Code { get; }            

        public TicketStatus Status { get; private set; }
        public string? OrderId { get; private set; } 
        public string? UserId { get; private set; } 

        public decimal Price { get; }
        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; private set; }

        private Ticket() { } 

        public Ticket(string eventId, string type, decimal price, string code)
        {
            EventId = eventId;
            Type = type;
            Price = price;
            Code = code;
            Status = TicketStatus.Available;
            CreatedAt = DateTime.UtcNow;
        }

        public void Reserve(string userId, string orderId)
        {
            if (Status != TicketStatus.Available)
                throw new DomainException("Ticket is already reserved!");

            Status = TicketStatus.Reserved;
            UserId = userId;
            OrderId = orderId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ConfirmPurchase()
        {
            if (Status != TicketStatus.Reserved)
                throw new DomainException("Only reserved ticket can be confirmed!");

            Status = TicketStatus.Sold;
            UpdatedAt = DateTime.UtcNow;
        }

        public void CancelReservation()
        {
            if (Status != TicketStatus.Reserved)
                throw new DomainException("Is not allowed to cancel no-reserved ticket!");

            Status = TicketStatus.Available;
            UserId = null;
            OrderId = null;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
