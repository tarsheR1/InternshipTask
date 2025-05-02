using UserManagementService.DataAccessLayer.Entities.Base;
using UserManagementService.DataAccessLayer.Entities.Role;
using UserManagementService.DataAccessLayer.Entities.Users;

namespace UserManagementService.DataAccessLayer.Entities.Relations
{
    public class UserRoleEntity : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public int RoleId { get; set; }
        public RoleEntity Role { get; set; }
    }
}
