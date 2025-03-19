using UserManagementService.Core.Models;

namespace UserManagementService.Core.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}