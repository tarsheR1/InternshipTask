using UserManagementService.Core.Models;

namespace UserManagementService.Application.Interfaces
{
    internal interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}