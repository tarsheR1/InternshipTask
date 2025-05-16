using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Auth
{
    public class InvalidCredentialsException : BusinessLogicException
    {
        public InvalidCredentialsException()
            : base("invalid_credentials",
                  "Неправильно ввёденый email или пароль") 
        { }
    }

}
