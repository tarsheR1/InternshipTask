using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Users
{
    class AlreadyExistsException : BusinessLogicException
    {

        public AlreadyExistsException(string identifier)
            : base("already_exist",
                  $"Запись уже существует: {identifier}",
                  409)
        { }
    }
}
