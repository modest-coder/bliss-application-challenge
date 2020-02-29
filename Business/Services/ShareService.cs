using System.Threading.Tasks;
using System.Net.Mail;

namespace Business.Services
{
    public class ShareService
    {
        private readonly EmailService _emailService;

        public ShareService(EmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task ShareLink(string destinationEmail, string contentUrl)
        {
            var mailMessage = new MailMessage
            {
                IsBodyHtml = true,
                Body = contentUrl,
                //Subject = ""
            };
            mailMessage.To.Add(new MailAddress(destinationEmail/*, ""*/));

            await _emailService.SendEmailAsync(mailMessage);
        }
    }
}
