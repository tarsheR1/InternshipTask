using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Users
{
    public interface IUserRoleService
    {
        Task AssignRoleToUserAsync(Guid userId, int roleId, CancellationToken cancellationToken = default);

        Task RemoveRoleFromUserAsync(Guid userId, int roleId, CancellationToken cancellationToken = default);

        Task<List<Role>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken = default);
    }

}