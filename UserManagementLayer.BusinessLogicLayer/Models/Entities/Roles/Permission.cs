namespace UserManagementService.BusinessLogicLayer.Models.Entities.Roles
{
    public class Permission
    {
        public Permission() { }

        public int Id { get; set; }
        public string Name { get; set; }             

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
