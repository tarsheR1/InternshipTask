using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Token
{
    public class TokenNotFoundException : BusinessLogicException
    {
        public TokenNotFoundException(string message)
            : base("token_not_found", 
                  message, 
                  404) 
        { }
    }
}
