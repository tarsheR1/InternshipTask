using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

namespace UserManagementService.BusinessLogicLayer.Models
{
    public class Permission
    {
        public Guid Id { get; set; }
        public string Name { get; set; }             

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
