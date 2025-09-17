namespace TicketManagementService.Application.DTO
{
    public record CreateTicketTypeResult(
     bool Success,
     Guid? TicketTypeId = null,
     string? ErrorMessage = null);
}
