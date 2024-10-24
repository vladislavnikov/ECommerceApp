using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using ECommerceApp.Business.Contract;

namespace ECommerceApp.Business.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = _configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var host = _configuration["Smtp:Host"];
            var port = _configuration["Smtp:Port"];
            var username = _configuration["Smtp:Username"];
            var password = _configuration["Smtp:Password"];
            var fromEmail = _configuration["Smtp:FromEmail"];

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(port) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(fromEmail))
            {
                throw new InvalidOperationException("SMTP configuration is missing or incomplete.");
            }

            var smtpClient = new SmtpClient(host)
            {
                Port = int.Parse(port),
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true,
            };

            await smtpClient.SendMailAsync(new MailMessage(fromEmail, email, subject, message));
        }

    }
}
