using UserManagementService.DataAccessLayer.Entities.Base;
using UserManagementService.DataAccessLayer.Entities.Role;

namespace UserManagementService.DataAccessLayer.Entities.Users
{
    public class UserEntity : BaseEntity<Guid>
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Phone { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public ICollection<RoleEntity> Roles{ get; set; } = new List<RoleEntity>();
    }
}