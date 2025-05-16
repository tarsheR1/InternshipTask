namespace UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string email, string confirmationLink);
    }
}
