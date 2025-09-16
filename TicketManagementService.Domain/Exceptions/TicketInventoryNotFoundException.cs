namespace TicketManagementService.Domain.Exceptions
{
    public class TicketInventoryNotFoundException : DomainException
    {
        public TicketInventoryNotFoundException(string ticketType)
            : base(
                code: "ticket_inventory.not_found",
                message: $"Тип билетов '{ticketType}' для указанного мероприятия",
                details: $"Выберите другое мероприятие или тип билетов")
        {
        }
    }
}
