using AspReactTestApp.DTOs;
using AspReactTestApp.ResponseModels;

namespace AspReactTestApp.Services.EmailService
{
    public interface IEmailService
    {
        public void SendEmail(string recipientEmail, string subject, string body);
        public string GenerateVerificationCode();
        public string GenerateVerificationEmail(string verificationCode);
        public bool CanRequestVerificationCode(string email);
        public void StoreVerificationCodeInCache(string email, string verificationCode);
        public EmailVerificationResult CheckVerificationCode(CheckVerificationCodeDto verificationCodeDto);
    }
}
