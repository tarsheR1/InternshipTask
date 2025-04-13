using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementService.DataAccessLayer.Entities
{
    [Table("roles")]
    public class RoleEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public ICollection<UserRoleEntity> UserRoles { get; set; }
        public ICollection<RolePermissionEntity> RolePermissions { get; set; }
    }
}
