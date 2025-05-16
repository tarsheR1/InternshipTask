namespace UserManagementService.BusinessLogicLayer.Exceptions.Core
{
    public class BusinessLogicException : Exception
    {
        public string ErrorCode { get; }

        public BusinessLogicException(string errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
