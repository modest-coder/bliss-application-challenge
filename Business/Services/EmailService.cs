using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Business.Settings;
using System.Net.Mail;
using System.Net;

namespace Business.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(MailMessage mailMessage)
        {
            mailMessage.From = new MailAddress(_emailSettings.FromEmail);
            using (var smtpClient = new SmtpClient(_emailSettings.SmtpServer))
            {
                smtpClient.Credentials = new NetworkCredential(_emailSettings.CredentialEmail, _emailSettings.CredentialPassword);
                smtpClient.Port = _emailSettings.SmtpPort;
                smtpClient.EnableSsl = true;

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
