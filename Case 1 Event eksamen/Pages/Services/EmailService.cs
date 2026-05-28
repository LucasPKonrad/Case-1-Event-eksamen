using System.Net;
using System.Net.Mail;

namespace Case_1_Event_eksamen.Pages.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(string toEmail, string subject, string body)
        {
            var smtpHost = _config["Email:SmtpHost"]!;
            var smtpPort = int.Parse(_config["Email:SmtpPort"]!);
            var fromEmail = _config["Email:FromEmail"]!;
            var password = _config["Email:Password"]!;

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true
            };

            var mail = new MailMessage(fromEmail, toEmail, subject, body)
            {
                IsBodyHtml = true
            };

            client.Send(mail);
        }

        public void SendToAll(List<string> emails, string subject, string body)
        {
            foreach (var email in emails)
            {
                try { SendEmail(email, subject, body); }
                catch { /* fortsæt til næste hvis én fejler */ }
            }
        }
    }
}