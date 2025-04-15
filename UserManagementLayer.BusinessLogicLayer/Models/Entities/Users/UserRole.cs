using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

namespace UserManagementService.BusinessLogicLayer.Models.Entities.Users
{
    public class UserRole
    {
        public Guid UserId { get; set; } 
        public User User { get; set; }    

        public int RoleId { get; set; }  
        public Role Role { get; set; }    

        public UserRole() { }
    }
}
