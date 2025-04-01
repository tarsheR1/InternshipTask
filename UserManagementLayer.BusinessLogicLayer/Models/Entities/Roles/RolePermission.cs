namespace UserManagementService.BusinessLogicLayer.Models.Entities.Roles
{
    public class RolePermission
    {
        public int RoleId { get; set; }      
        public Role Role { get; set; }       

        public int PermissionId { get; set; } 
        public Permission Permission { get; set; }
    }
}
