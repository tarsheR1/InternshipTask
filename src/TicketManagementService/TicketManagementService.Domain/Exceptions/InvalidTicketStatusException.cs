using TicketManagementService.Domain.Enums;

namespace TicketManagementService.Domain.Exceptions
{
    public class InvalidTicketStatusException : DomainException
    {
        public InvalidTicketStatusException(TicketStatus currentStatus)
            : base(
                code: "tickets.invalid_status",
                message: $"Недопустимый статус билета: {currentStatus}",
                details: "Билет должен быть в статусе 'Reserved' для оплаты")
        {
        }
    }
}
