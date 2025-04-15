using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementService.DataAccessLayer.Entities
{
    public class PermissionEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<RolePermissionEntity> RolePermissions { get; set; }
    }
}
