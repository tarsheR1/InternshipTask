using Microsoft.Extensions.Options;
using System.Net.Mail;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;

namespace UserManagementService.BusinessLogicLayer.Services.ExternalServices
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailService(IOptions<EmailConfiguration> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        public async Task SendConfirmationEmailAsync(string email, string confirmationLink)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your App", _emailConfig.From));
            message.To.Add(new MailboxAddress("User", email));
            message.Subject = "Confirm your registration";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>."
            };

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
            await client.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
