using AspReactTestApp.DTO;

namespace AspReactTestApp.Services.EmailService;

public interface IEmailService
{
    public void SendEmail(string recipientEmail, string subject, string body);
    public string GenerateVerificationCode();
    public string GenerateVerificationEmail(string verificationCode);
    public ResponseDTO SendVerificationCode(string recipientEmail); 
    public bool CanRequestVerificationCode(string email);
    public void StoreVerificationCodeInCache(string email, string verificationCode);
    public ResponseDTO CheckVerificationCode(CheckVerificationCodeDTO verificationCodeDto);
}
