namespace UserManagementService.BusinessLogicLayer.Models.Commands
{
    public class UserLoginCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
