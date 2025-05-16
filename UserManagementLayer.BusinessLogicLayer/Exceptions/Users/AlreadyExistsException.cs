using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Users
{
    public class AlreadyExistsException : BusinessLogicException
    {
        public AlreadyExistsException(string identifier)
            : base("already_exist",
                  $"Запись уже существует: {identifier}")
        { }
    }
}
