using UserManagementService.BusinessLogicLayer.Models.Entities.Users;

namespace UserManagementService.BusinessLogicLayer.Models.Entities.Roles
{
    public class Role
    {
        public Role() { }

        public int Id { get; set; }
        public string Name { get; set; }               

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
