namespace TicketManagementService.Application.DTO
{
    public record ReserveTicketsRequest(
    Guid EventId,
    string TicketType,
    int Quantity,
    Guid UserId
);

}
