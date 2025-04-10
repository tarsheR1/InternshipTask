using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Users
{
    class UserNotFoundException : BusinessLogicException
    {
        public UserNotFoundException(string identifier)
            : base("user_not_found",
                  $"Пользователь не найден: {identifier}",
                  404) 
        { }
    }

}
