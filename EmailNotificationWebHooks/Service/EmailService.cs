using MailKit.Net.Smtp;
using MimeKit;
using Shared.DTOs;


namespace EmailNotificationWebHooks.Service
{
    public class EmailService : IEmailService
    {
        public string SendEamil(EmailDTO emailDTo)
        {
          var _email =  new MimeMessage();
            _email.From.Add(MailboxAddress.Parse(""));
            _email.To.Add(MailboxAddress.Parse(""));
            _email.Subject = emailDTo.Title;
            _email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailDTo.Content };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("", "",CancellationToken.None);
            smtp.Send(_email, CancellationToken.None);
            smtp.Disconnect(true);
            return "Email Sent Successfully";
        }
    }
}
