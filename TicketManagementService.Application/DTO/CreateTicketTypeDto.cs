namespace TicketManagementService.Application.DTO
{
    public class CreateTicketTypeDto
    {
        public Guid EventId { get; set; }
        public string TicketType { get; set; }
        public int TotalQuantity { get; set; }
    }

}
