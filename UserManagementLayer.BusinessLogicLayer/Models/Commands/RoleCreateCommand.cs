using System.ComponentModel.DataAnnotations;

namespace UserManagementService.BusinessLogicLayer.Models.Commands
{
    public class RoleCreateCommand
    {
        [Required]
        public string Name { get; set; }
    }
}
