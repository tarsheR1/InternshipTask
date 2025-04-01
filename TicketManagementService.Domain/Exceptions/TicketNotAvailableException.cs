namespace TicketManagementService.Domain.Exceptions
{
    public class TicketNotAvailableException : DomainException
    {
        public string TicketId { get; }

        public TicketNotAvailableException(string ticketId)
            : base($"Ticket {ticketId} is not available")
        {
            TicketId = ticketId;
        }
    }
}
