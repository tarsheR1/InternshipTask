using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.BusinessLogicLayer.Models.Commands;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Users
{
    public interface IRoleService
    {
        Task CreateRoleAsync(RoleCreateCommand command, CancellationToken cancellationToken);

        Task<Role> UpdateRoleAsync(int roleId, RoleUpdateCommand command, CancellationToken cancellationToken);

        Task DeleteRoleAsync(int roleId, CancellationToken cancellationToken);

        Task<Role> GetRoleByIdAsync(int roleId, CancellationToken cancellationToken);

        Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken cancellationToken);
    }
}
