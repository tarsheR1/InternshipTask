using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Auth
{
    public class EmailAlreadyExistsException : BusinessLogicException
    {
        public EmailAlreadyExistsException(string email)
            : base("email_already_exists",
                  $"Пользователь с электрнной почтой '{email}' уже существует") 
        { }
    }
}
