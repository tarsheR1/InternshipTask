namespace TicketManagementService.API.DTOs
{
    public record ReserveTicketsRequest(
    Guid EventId,
    string TicketType,
    int Quantity,
    Guid UserId
);

}
