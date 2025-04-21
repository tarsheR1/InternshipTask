using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Users
{
    public class NotFoundException : BusinessLogicException
    {
        public NotFoundException(string identifier)
            : base("not_found",
                  $"Запись не найдена: {identifier}")
        { }
    }
}
