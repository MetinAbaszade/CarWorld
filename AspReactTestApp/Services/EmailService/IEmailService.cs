using AspReactTestApp.DTOs;

namespace AspReactTestApp.Services.EmailService
{
    public interface IEmailService
    {
        public void SendEmail(string recipientEmail, string subject, string body);
    }
}
