namespace UserManagementService.Core.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }               

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
