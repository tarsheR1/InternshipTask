using UserManagementService.DataAccessLayer.Entities.Relations;

namespace UserManagementService.DataAccessLayer.Entities.Role
{
    public class PermissionEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<RolePermissionEntity> RolePermissions { get; set; }
    }
}
