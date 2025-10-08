namespace UserManagementService.DataAccessLayer.Entities
{
    public class UserRoleEntity
    {
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public int RoleId { get; set; }
        public RoleEntity Role { get; set; }
    }
}
