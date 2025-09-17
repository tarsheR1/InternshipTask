namespace TicketManagementService.Domain.Exceptions
{
    public class TicketsSoldOutException : DomainException
    {
        public TicketsSoldOutException(string ticketType, int quantity)
            : base(
                code: "tickets.sold_out",
                message: $"Билетов типа '{ticketType}' недостаточно",
                details: $"Измените количесвто или выберите другой тип билетов")
        {
        }
    }
}
