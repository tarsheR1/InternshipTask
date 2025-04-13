using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserManagementService.DataAccessLayer.Entities
{
    [Table("users")] 
    public class UserEntity
    {
        [Key] 
        [Column("id")] 
        public Guid Id { get; set; }

        [Required] 
        [Column("email")]
        [MaxLength(255)] 
        public string Email { get; set; }

        [Required]
        [Column("password_hash")]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        [Column("first_name")]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [Column("last_name")]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Column("middle_name")]
        [MaxLength(100)]
        public string? MiddleName { get; set; }

        [Column("phone")]
        [MaxLength(15)]
        public string? Phone { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();

        public UserEntity() { }
    }
}