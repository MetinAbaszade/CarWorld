using System.Net.Mail;
using System.Net;
using AspReactTestApp.DTOs;
using Microsoft.Extensions.Caching.Memory; 

namespace AspReactTestApp.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;

        public EmailService(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        public void SendEmail(string recipientEmail, string subject, string body)
        {
            string email = _configuration["EmailSettings:Email"];
            string password = _configuration["EmailSettings:Password"];
            string smtpClientName = _configuration["EmailSettings:SmtpClient"];
            string smtpPort = _configuration["EmailSettings:SmtpPort"];

            try
            {
                // Configure the SMTP client
                using (SmtpClient smtpClient = new SmtpClient(smtpClientName))
                {
                    smtpClient.Port = int.Parse(smtpPort);
                    smtpClient.Credentials = new NetworkCredential(email, password);
                    smtpClient.EnableSsl = true; // Enable SSL for Gmail

                    using (MailMessage message = new MailMessage())
                    {
                        message.From = new MailAddress(email);
                        message.Subject = subject;
                        message.To.Add(recipientEmail);
                        message.IsBodyHtml = true;
                        message.Body = body;

                        smtpClient.Send(message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GenerateVerificationCode method to create a new verification code
        public string GenerateVerificationCode()
        {
            var random = new Random();
            return random.Next(100000, 1000000).ToString();
        }

        // GenerateVerificationEmail method to create an email with specified verification code
        public string GenerateVerificationEmail(string verificationCode)
        {
            var email = @$"<div style=""font-size: 20px;"">
Thank you for choosing CarUniverse as your preferred platform for all your automotive needs. As part of our security measures, we require all users to verify their account before gaining full access to our services.<br><br>

Your verification code is: <b>{verificationCode}</b><br><br>

Please enter this code in the designated field on the CarUniverse app or website to complete the verification process. Please note that this code is only valid for the next 2 minutes. After that, it will expire and you will need to request a new code.<br><br>

If you did not request this verification code, please disregard this message.<br><br>

If you have any questions or concerns, please do not hesitate to contact our customer support team for assistance. We are available 24/7 to assist you.<br>

Thank you for choosing CarUniverse.<br><br>

Best regards,<br>
The CarUniverse Team

</div>";

            return email;
        }

        // CanRequestVerificationCode method to determine whether the user can request a new verification code
        public bool CanRequestVerificationCode(string email)
        {
            if (!_memoryCache.TryGetValue($"{email}_lastRequest", out DateTime lastRequestTimestamp))
            {
                // No previous request, allow the current request
                _memoryCache.Set($"{email}_lastRequest", DateTime.UtcNow);
                return true;
            }

            var timeSinceLastRequest = DateTime.UtcNow - lastRequestTimestamp;

            if (timeSinceLastRequest.TotalMinutes <= 1)
            {
                // Last request was within the last minute, disallow the current request
                return false;
            }

            // Last request was more than a minute ago, update the timestamp and allow the current request
            _memoryCache.Set($"{email}_lastRequest", DateTime.UtcNow);
            return true;
        }

        // SendVerificationCode method to send a verification code to a user's email
        public ResponseDto SendVerificationCode(string recipientEmail)
        {
            try
            {
                if (!CanRequestVerificationCode(recipientEmail))
                {
                    Dictionary<string, string> errors = new();
                    var errorMessage = "You have requested too many verification code, please try again later";
                    errors["email"] = errorMessage;
                    return new ResponseDto
                    {
                        IsSuccessfull = false,
                        Message = errorMessage,
                        Errors = errors
                    };
                }

                var verificationCode = GenerateVerificationCode();
                var verificationEmailBody = GenerateVerificationEmail(verificationCode);
                var verificationEmailSubject = "Verification Code for CarUniverse Account (Expires in 2 Minutes)";

                SendEmail(recipientEmail, verificationEmailSubject, verificationEmailBody);
                StoreVerificationCodeInCache(recipientEmail, verificationCode);


                return new ResponseDto
                {
                    IsSuccessfull = true,
                    Message = "Verification sent successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    Message = ex.Message
                };
            }
        }

        // StoreVerificationCodeInCache method to store the generated verification code in the cache
        public void StoreVerificationCodeInCache(string email, string verificationCode)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
            _memoryCache.Set(email, verificationCode, cacheEntryOptions);
        }

        // CheckVerificationCode method to validate the user-provided verification code
        public ResponseDto CheckVerificationCode(CheckVerificationCodeDto verificationCodeDto)
        {
            var keyExists = _memoryCache.TryGetValue(verificationCodeDto.Email, out string storedVerificationCode);
            Dictionary<string, string> errors = new();
            var errorMessage = string.Empty;

            if (!keyExists)
            {
                errorMessage = "Please request a new code.";
                errors["verificationCode"] = errorMessage;

                return new ResponseDto
                {
                    Message = errorMessage,
                    Errors = errors
                };
            }

            if (storedVerificationCode == verificationCodeDto.VerificationCode)
            {
                return new ResponseDto
                {
                    IsSuccessfull = true,
                    Message = "Email Verification successful",
                    Errors = errors
                };
            }

            errorMessage = "Incorrect verification code entered.";
            errors["verificationCode"] = errorMessage;
            return new ResponseDto
            {
                Message = errorMessage,
                Errors = errors
            };
        }


    }
}
