namespace UserManagementService.Core.Models
{
    public class Permission
    {
        public Guid Id { get; set; }
        public string Name { get; set; }             

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
