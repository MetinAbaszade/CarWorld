using System.Net.Mail;
using System.Net;

namespace AspReactTestApp.Services.EmailService
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                // Configure the SMTP client
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587; // Gmail uses port 587 for TLS connections
                smtpClient.Credentials = new NetworkCredential("metinabaszade.h@gmail.com", "ghzwjllkuyvkfwsr");
                smtpClient.EnableSsl = true; // Enable SSL for Gmail

                MailMessage message = new MailMessage();
                message.From = new MailAddress("metinabaszade.h@gmail.com");
                message.Subject = subject;
                message.To.Add(recipientEmail);
                message.IsBodyHtml = true;

                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
