using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RentEase.Application.Contracts.Infrastructure;
using RentEase.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace RentEase.Infrastructure.Email
{
    public class EmailService: IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmail(Application.Models.Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);
            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;
            var from = new EmailAddress(_emailSettings.FromAddress, _emailSettings.FromName);
            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted ||
                response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            _logger.LogError($"Failed to send email. Status code: {response.StatusCode}, Body: {await response.Body.ReadAsStringAsync()}");
            return false;
        }
    }
}
