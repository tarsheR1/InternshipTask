using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Auth
{
    public class InvalidRefreshTokenException : BusinessLogicException
    {
        public InvalidRefreshTokenException()
            : base("invalid_refresh_token",
                  "Невалидный refresh token") 
        { }
    }

}
