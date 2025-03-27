using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

namespace UserManagementService.BusinessLogicLayer.Models
{
    public class UserRole
    {
        public Guid UserId { get; set; } 
        public User User { get; set; }    

        public Guid RoleId { get; set; }  
        public Role Role { get; set; }    
    }
}
