using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;
namespace MulakatCalisma.Helper
{
    public class EmailSender : IEmailSender
    {
        public string SendGridSecret { get; set; }

        public EmailSender(IConfiguration _config)
        {
            SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                //var client = new SendGridClient(SendGridSecret);
                //var from = new EmailAddress("Assos@dotnetmaster.com", "Assos");
                //var to = new EmailAddress(email);
                //var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
                //await client.SendEmailAsync(msg);
                var emailToSend = new MimeMessage();
                emailToSend.From.Add(MailboxAddress.Parse("Assos@dotnetmaster.com"));
                emailToSend.To.Add(MailboxAddress.Parse(email));
                emailToSend.Subject = subject;
                emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

                ////send email
                using var emailClient = new SmtpClient();
                emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("assossoft@gmail.com", "opmiuuatzstkxoqj");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
