using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Token
{
    class TokenRevocationException : BusinessLogicException
    {
        public TokenRevocationException(string message)
           : base("token_revocation_failed", 
                 message, 
                 500) 
        { }
    }
}
