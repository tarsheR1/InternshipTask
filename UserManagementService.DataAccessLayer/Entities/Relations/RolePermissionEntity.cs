using UserManagementService.DataAccessLayer.Entities.Base;
using UserManagementService.DataAccessLayer.Entities.Role;

namespace UserManagementService.DataAccessLayer.Entities.Relations
{
    public class RolePermissionEntity : BaseEntity<int>
    {
        public int RoleId { get; set; }
        public RoleEntity Role { get; set; }

        public int PermissionId { get; set; }
        public PermissionEntity Permission { get; set; }
    }
}
