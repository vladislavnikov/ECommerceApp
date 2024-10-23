using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace ECommerceApp.Business.Services
{
    public class EmailService
    {
        private readonly IConfiguration configuration;
        public EmailService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient(configuration["Smtp:Host"])
            {
                Port = int.Parse(configuration["Smtp:Port"]),
                Credentials = new NetworkCredential(configuration["Smtp:Username"], configuration["Smtp:Password"]),
                EnableSsl = true,
            };

            await smtpClient.SendMailAsync(new MailMessage(configuration["Smtp:FromEmail"], email, subject, message));
        }
    }
}
