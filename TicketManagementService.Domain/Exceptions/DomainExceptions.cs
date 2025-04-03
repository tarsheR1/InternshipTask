using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementService.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        public string Code { get; } 
        public string Details { get; } 

        protected DomainException(string code, string message, string details = null)
            : base(message)
        {
            Code = code;
            Details = details ?? message;
        }
    }
}
