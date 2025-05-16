using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Token
{
    public class TokenGenerationException : BusinessLogicException
    {
        public TokenGenerationException(string message)
           : base("token_generation_failed", 
                 message) 
        { }
    }
}
