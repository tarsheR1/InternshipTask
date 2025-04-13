using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementService.DataAccessLayer.Entities
{
    [Table("user_roles")]
    public class UserRoleEntity
    {
        [Column("user_id")]
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }
        public RoleEntity Role { get; set; }
    }
}
