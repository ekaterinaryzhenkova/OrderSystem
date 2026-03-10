using Microsoft.Extensions.Options;

using NotificationService.Application;
using System.Net;
using System.Net.Mail;


namespace NotificationService.Business
{
    public class EmailService(IOptions<EmailOptions> options) : IEmailService
    {
        public async Task SendEmailAsync(string to, string body)
        {
            var optionsValue = options.Value;
            var client = new SmtpClient(optionsValue.Host, optionsValue.Port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(optionsValue.Sender, optionsValue.AppPassword),
                EnableSsl = true
            };

            var message = new MailMessage{
                From = new MailAddress(optionsValue.Sender),
                Subject = optionsValue.Subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(to));
            
            await client.SendMailAsync(message);
            
            /*var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(optionsValue.Sender));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = optionsValue.Subject;

            email.Body = new TextPart(TextFormat.Text)
            {
                Text = body
            };

            using var smtp = new SmtpClient();
            
            await smtp.ConnectAsync(optionsValue.Host, optionsValue.Port, SecureSocketOptions.Auto);

            await smtp.AuthenticateAsync(
                optionsValue.Sender,
                optionsValue.AppPassword);

            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);*/
        }
    }
}