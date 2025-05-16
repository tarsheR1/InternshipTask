using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Users
{
    public interface IUserRoleService
    {
        Task AssignRoleToUserAsync(Guid userId, int roleId, CancellationToken cancellationToken);
        Task RemoveRoleFromUserAsync(Guid userId, int roleId, CancellationToken cancellationToken);
        Task<List<Role>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken);
    }
}