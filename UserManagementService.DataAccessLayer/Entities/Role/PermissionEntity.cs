using UserManagementService.DataAccessLayer.Entities.Base;

namespace UserManagementService.DataAccessLayer.Entities.Role
{
    public class PermissionEntity : BaseEntity<int>
    {
        public string Name { get; set; }
        
        public ICollection<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    }
}
