using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Token
{
    public class InvalidTokenException : BusinessLogicException
    {
        public InvalidTokenException(string message)
          : base("invalid_token_format", 
                message) 
        { }
    }
}
