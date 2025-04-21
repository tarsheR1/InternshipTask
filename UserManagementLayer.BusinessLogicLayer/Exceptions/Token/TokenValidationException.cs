using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Token
{
    public class TokenValidationException : BusinessLogicException
    {
        public TokenValidationException(string message)
            : base("token_validation_failed", 
                  message) 
        { }
    }
}
