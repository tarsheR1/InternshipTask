namespace TicketManagementService.Application.DTO
{
    public class TicketTypeCreatedDto
    {
        public bool Success { get; set; }
        public Guid? TicketTypeId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
