using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Users
{
    public class ConflictException : BusinessLogicException
    {
        public ConflictException(string message)
            : base("conflict",
                  message)
        { }
    }
}
