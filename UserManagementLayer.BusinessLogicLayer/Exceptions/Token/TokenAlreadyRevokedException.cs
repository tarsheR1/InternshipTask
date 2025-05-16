using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Token
{
    public class TokenAlreadyRevokedException : BusinessLogicException
    {
        public TokenAlreadyRevokedException(string message)
            : base("token_already_revoked", 
                  message)
        { }
    }
}
