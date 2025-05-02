using UserManagementService.DataAccessLayer.Entities.Base;
using UserManagementService.DataAccessLayer.Entities.Relations;

namespace UserManagementService.DataAccessLayer.Entities.Role
{
    public class RoleEntity : BaseEntity<int>
    {
        public string Name { get; set; }

        public ICollection<UserRoleEntity> UserRoles { get; set; }
        public ICollection<RolePermissionEntity> RolePermissions { get; set; }
    }
}
