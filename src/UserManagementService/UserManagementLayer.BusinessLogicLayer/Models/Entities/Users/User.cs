namespace UserManagementService.BusinessLogicLayer.Models.Entities.Users
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string? MiddleName { get; private set; }
        public string? Phone { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

        public User(
            string email,
            string passwordHash,
            string firstName,
            string lastName,
            string? middleName = null,
            string? phone = null)
        {
            Id = Guid.NewGuid();
            Email = email?.Trim().ToLower() ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            FirstName = firstName?.Trim() ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName?.Trim() ?? throw new ArgumentNullException(nameof(lastName));
            MiddleName = middleName?.Trim();
            Phone = phone?.Trim();
            CreatedAt = DateTime.UtcNow;
        }

        private User() { }
    }
}
