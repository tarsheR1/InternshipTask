namespace UserManagementService.BusinessLogicLayer.Models.Entities.Roles
{
    public class Permission
    {
        public Guid Id { get; set; }
        public string Name { get; set; }             

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
