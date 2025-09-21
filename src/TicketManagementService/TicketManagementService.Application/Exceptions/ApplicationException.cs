namespace TicketManagementService.Application.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        public string Code { get; }
        public string Details { get; }

        protected ApplicationException(string code, string message, string details = null)
            : base(message)
        {
            Code = code;
            Details = details ?? message;
        }
    }
}
