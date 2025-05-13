using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request.Roles;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Users
{
    public interface IRoleService
    {
        Task CreateRoleAsync(RoleCreateRequestDto command, CancellationToken cancellationToken);

        Task<Role> UpdateRoleAsync(int roleId, RoleUpdateRequestDto command, CancellationToken cancellationToken);

        Task DeleteRoleAsync(int roleId, CancellationToken cancellationToken);

        Task<Role> GetRoleByIdAsync(int roleId, CancellationToken cancellationToken);

        Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken cancellationToken);
    }
}
