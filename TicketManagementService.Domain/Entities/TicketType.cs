using TicketManagementService.Domain.Common;
using TicketManagementService.Domain.Exceptions;

namespace TicketManagementService.Domain.Entities
{
    public class TicketType : BaseEntity<Guid>
    {
        public Guid EventId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int QuantityAvailable { get; private set; }
        public DateTime? SaleStartDate { get; private set; }
        public DateTime? SaleEndDate { get; private set; }
        public bool IsActive { get; private set; }

        private TicketType() { }

        public TicketType(
            Guid eventId,
            string name,
            string description,
            decimal price,
            int quantityAvailable,
            DateTime? saleStartDate,
            DateTime? saleEndDate)
        {
            EventId = eventId;
            Name = name;
            Description = description;
            Price = price;
            QuantityAvailable = quantityAvailable;
            SaleStartDate = saleStartDate;
            SaleEndDate = saleEndDate;
            IsActive = true;
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity < 0)
                throw new DomainException("Quantity cannot be negative");

            QuantityAvailable = newQuantity;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}