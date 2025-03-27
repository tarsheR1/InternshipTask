using UserManagementService.BusinessLogicLayer.Models;

namespace UserManagementService.BusinessLogicLayer.Services.Interfaces.Infrastructure
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}