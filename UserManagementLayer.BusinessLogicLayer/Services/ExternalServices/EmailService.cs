using Hangfire;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;
using UserManagementService.BusinessLogicLayer.Models.Settings;


public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailService> _logger;
    private readonly string _baseUrl;

    public EmailService(
        IOptions<EmailSettings> emailSettings,
        IConfiguration config,
        ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
        _baseUrl = config["AppSettings:BaseUrl"]
            ?? throw new ArgumentNullException("BaseUrl is not configured");
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;

            message.Body = new TextPart(TextFormat.Html) { Text = body };

            using var client = new SmtpClient();

            await client.ConnectAsync(
                _emailSettings.SmtpServer,
                _emailSettings.SmtpPort,
                SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(
                _emailSettings.SmtpUsername,
                _emailSettings.SmtpPassword);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation($"Email sent to {toEmail}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error sending email to {toEmail}");
            throw;
        }
    }

    [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
    public async Task SendConfirmationEmailAsync(string email, Guid userId, string token)
    {
        try
        {
            var confirmationUrl = $"{_baseUrl}/api/auth/confirm-email?userId={userId}&token={Uri.EscapeDataString(token)}";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = "Подтвердите ваш email";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = CreateEmailTemplate(confirmationUrl)
            };

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation($"Confirmation email sent to {email}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to send confirmation email to {email}");
            throw;
        }
    }

    private string CreateEmailTemplate(string confirmationUrl)
    {
        return $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                    <h2 style='color: #2c3e50;'>Добро пожаловать!</h2>
                    <p>Для завершения регистрации подтвердите ваш email:</p>
                    <a href='{confirmationUrl}' 
                       style='display: inline-block; padding: 10px 20px; 
                              background-color: #3498db; color: white; 
                              text-decoration: none; border-radius: 5px;'>
                        Подтвердить email
                    </a>
                    <p style='margin-top: 20px; color: #7f8c8d;'>
                        Если вы не регистрировались, проигнорируйте это письмо.
                    </p>
                    <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;'>
                    <p style='font-size: 12px; color: #95a5a6;'>
                        Ссылка действительна 24 часа.
                    </p>
                </div>";
    }
}

