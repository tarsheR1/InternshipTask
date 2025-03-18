namespace UserManagementService.Core.Models
{
    public class User
    {
        public Guid Id { get; set; } 
        public string Email { get; set; }            
        public string PasswordHash { get; set; }      
        public string FirstName { get; set; }         
        public string LastName { get; set; }          
        public string MiddleName { get; set; }        
        public string Phone { get; set; }             
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
