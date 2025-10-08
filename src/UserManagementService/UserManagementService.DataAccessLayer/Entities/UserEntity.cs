namespace UserManagementService.DataAccessLayer.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public ICollection<UserRoleEntity> UserRoles { get; set; }
        public ICollection<RefreshTokenEntity> RefreshTokens { get; set; }
    } 
}
