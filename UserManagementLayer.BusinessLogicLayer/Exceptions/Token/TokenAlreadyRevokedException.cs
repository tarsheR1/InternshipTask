using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Token
{
    class TokenAlreadyRevokedException : BusinessLogicException
    {
        public TokenAlreadyRevokedException(string message)
            : base("token_already_revoked", 
                  message, 
                  409)
        { }
    }
}
