using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.BusinessLogicLayer.Models.Entities.Users
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string? Phone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public User() { }
    }
}
