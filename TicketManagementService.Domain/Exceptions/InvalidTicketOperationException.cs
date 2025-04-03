using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementService.Domain.Exceptions
{
    class InvalidTicketOperationException : DomainException
    {
        public InvalidTicketOperationException() : base(
                code: "tickets.invalid_status",
                message: $"Недопустимый статус билета:",
                details: "Билет должен быть в статусе 'Reserved' для оплаты")
        {
        }
    }
}
