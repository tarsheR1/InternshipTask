using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementService.DataAccessLayer.Entities
{
    [Table("permissions")]
    public class PermissionEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public ICollection<RolePermissionEntity> RolePermissions { get; set; }
    }
}
