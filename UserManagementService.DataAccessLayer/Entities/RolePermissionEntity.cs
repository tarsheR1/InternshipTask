using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementService.DataAccessLayer.Entities
{
    [Table("role_permissions")]
    public class RolePermissionEntity
    {
        [Column("role_id")]
        public int RoleId { get; set; }
        public RoleEntity Role { get; set; }

        [Column("permission_id")]
        public int PermissionId { get; set; }
        public PermissionEntity Permission { get; set; }
    }

}
