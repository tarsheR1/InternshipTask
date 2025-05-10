using UserManagementService.DataAccessLayer.Entities.Base;
using UserManagementService.DataAccessLayer.Entities.Relations;
using UserManagementService.DataAccessLayer.Entities.Users;

namespace UserManagementService.DataAccessLayer.Entities.Role
{
    public class RoleEntity : BaseEntity<int>
    {
        public string Name { get; set; }

        public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
        public ICollection<PermissionEntity> Permissions { get; set; } = new List<PermissionEntity>();
    }
}
