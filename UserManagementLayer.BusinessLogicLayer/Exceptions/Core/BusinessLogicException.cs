namespace UserManagementService.BusinessLogicLayer.Exceptions.Core
{
    public class BusinessLogicException : Exception
    {
        public string ErrorCode { get; }
        public int HttpStatusCode { get; }

        public BusinessLogicException(string errorCode, string message, int httpStatusCode)
            : base(message)
        {
            ErrorCode = errorCode;
            HttpStatusCode = httpStatusCode;
        }
    }
}
