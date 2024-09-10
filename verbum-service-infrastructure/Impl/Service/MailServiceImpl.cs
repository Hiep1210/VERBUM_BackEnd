using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using verbum_service_application.Service;
using verbum_service_domain.Models.Mail;

namespace verbum_service_infrastructure.Impl.Service
{
    public class MailServiceImpl : MailService
    {
        private readonly MailSettings mailSettings;
        public MailServiceImpl(IOptions<MailSettings> _mailSettings)
        {
            mailSettings = _mailSettings.Value;
        }
        public async Task SendEmailAsync(string receiver, string subject, string body)
        {
            await SendMail(new MailContent()
            {
                To = receiver,
                Subject = subject,
                Body = body
            });
        }

        private async Task SendMail(MailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
            email.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;
            
            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            // dùng SmtpClient của MailKit
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(email);

                smtp.Disconnect(true);
            }
        }
    }
}
