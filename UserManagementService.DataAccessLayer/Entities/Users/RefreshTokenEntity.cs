namespace UserManagementService.DataAccessLayer.Entities.Users
{
    public class RefreshTokenEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }

        public UserEntity User { get; set; }
    }
}
