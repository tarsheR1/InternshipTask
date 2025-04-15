using System.ComponentModel.DataAnnotations;

namespace UserManagementService.BusinessLogicLayer.Models.Commands
{
    public class RoleUpdateCommand
    {
        [Required]
        public string Name { get; set; }
    }
}
