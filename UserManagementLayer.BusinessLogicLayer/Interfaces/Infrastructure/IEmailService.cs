namespace UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
        Task SendConfirmationEmailAsync(string email, Guid userId, string token);
    }
}
