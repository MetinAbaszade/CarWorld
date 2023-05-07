using AspReactTestApp.DTOs;
using AspReactTestApp.ResponseModels;

namespace AspReactTestApp.Services.EmailService
{
    public interface IEmailService
    {
        public void SendEmail(string recipientEmail, string subject, string body);
        public EmailVerificationResult SendVerificationCode(string recipientEmail);
        public EmailVerificationResult CheckVerificationCode(CheckVerificationCodeDto verificationCodeDto);
    }
}
