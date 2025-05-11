namespace UserManagementService.BusinessLogicLayer.Models.Entities.Roles
{
    public class Permission
    {
        public Permission() { }

        public int Id { get; set; }
        public string Name { get; set; }             

        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
