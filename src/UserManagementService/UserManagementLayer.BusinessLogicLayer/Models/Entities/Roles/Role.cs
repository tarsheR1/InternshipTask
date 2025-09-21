using UserManagementService.BusinessLogicLayer.Models.Entities.Users;

namespace UserManagementService.BusinessLogicLayer.Models.Entities.Roles
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }               

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
