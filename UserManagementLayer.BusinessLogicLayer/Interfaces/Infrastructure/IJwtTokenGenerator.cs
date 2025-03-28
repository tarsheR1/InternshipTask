using UserManagementService.BusinessLogicLayer.Models.Entities.Users;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}