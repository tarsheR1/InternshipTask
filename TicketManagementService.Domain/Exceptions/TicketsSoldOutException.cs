namespace TicketManagementService.Domain.Exceptions
{
    public class TicketsSoldOutException : DomainException
    {
        public TicketsSoldOutException(string ticketType)
            : base(
                code: "tickets.sold_out",
                message: $"Билеты типа '{ticketType}' распроданы",
                details: $"Попробуйте выбрать другой тип билета или мероприятие")
        {
        }
    }
}
