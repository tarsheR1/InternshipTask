using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Users
{
    public interface IRolePermissionAssignmentService
    {
        Task AssignPermissionToRoleAsync(int roleId, int permissionId, CancellationToken cancellationToken );

        Task RemovePermissionFromRoleAsync(int roleId, int permissionId, CancellationToken cancellationToken);

        Task<IEnumerable<Permission>> GetRolePermissionsAsync(int roleId, CancellationToken cancellationToken);
    }
}
