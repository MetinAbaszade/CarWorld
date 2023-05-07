using System.Net.Mail;
using System.Net;
using AspReactTestApp.DTOs;
using Microsoft.Extensions.Caching.Memory;
using AspReactTestApp.CustomExceptions;
using AspReactTestApp.ResponseModels;

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
        private string GenerateVerificationCode()
        {
            var random = new Random();
            return random.Next(100000, 1000000).ToString();
        }

        // CanRequestVerificationCode method to determine whether the user can request a new verification code
        private bool CanRequestVerificationCode(string email)
        {
            if (!_memoryCache.TryGetValue($"{email}_lastRequest", out DateTime lastRequestTimestamp))
            {
                // No previous request, allow the current request
                _memoryCache.Set($"{email}_lastRequest", DateTime.UtcNow);
                return true;
            }

            var timeSinceLastRequest = DateTime.UtcNow - lastRequestTimestamp;

            if (timeSinceLastRequest.TotalMinutes >= 1)
            {
                // Last request was more than a minute ago, update the timestamp and allow the current request
                _memoryCache.Set($"{email}_lastRequest", DateTime.UtcNow);
                return true;
            }

            // Last request was within the last minute, disallow the current request
            return false;
        }

        // StoreVerificationCodeInCache method to store the generated verification code in the cache
        private void StoreVerificationCodeInCache(string email, string verificationCode)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
            _memoryCache.Set(email, verificationCode, cacheEntryOptions);
        }

        // SendVerificationCode method to send a verification code to a user's email
        public EmailVerificationResult SendVerificationCode(string recipientEmail)
        {
            if (!CanRequestVerificationCode(recipientEmail))
            {
                return new EmailVerificationResult
                {
                    IsSuccessful = false,
                    Message = "You have requested too many verification code, please try again later"
                };
            }

            var subject = "Verification Code for CarUniverse Account (Expires in 2 Minutes)";

            var verificationCode = GenerateVerificationCode();
            StoreVerificationCodeInCache(recipientEmail, verificationCode);

            var body = @$"<div style=""font-size: 20px;"">
Thank you for choosing CarUniverse as your preferred platform for all your automotive needs. As part of our security measures, we require all users to verify their account before gaining full access to our services.<br><br>

Your verification code is: <b>{verificationCode}</b><br><br>

Please enter this code in the designated field on the CarUniverse app or website to complete the verification process. Please note that this code is only valid for the next 2 minutes. After that, it will expire and you will need to request a new code.<br><br>

If you did not request this verification code, please disregard this message.<br><br>

If you have any questions or concerns, please do not hesitate to contact our customer support team for assistance. We are available 24/7 to assist you.<br>

Thank you for choosing CarUniverse.<br><br>

Best regards,<br>
The CarUniverse Team

</div>";

            try
            {
                SendEmail(recipientEmail, subject, body);

                return new EmailVerificationResult
                {
                    IsSuccessful = true,
                    Message = "Email Sent Successfully"
                };
            }
            catch (Exception ex)
            {
                throw new VerificationEmailSendingException(ex.Message);
            }
        }

        // CheckVerificationCode method to validate the user-provided verification code
        public EmailVerificationResult CheckVerificationCode(CheckVerificationCodeDto verificationCodeDto)
        {
            var keyExists = _memoryCache.TryGetValue(verificationCodeDto.Email, out string storedVerificationCode);

            if (!keyExists)
            {
                return new EmailVerificationResult
                {
                    IsSuccessful = false,
                    Message = "Please request a new code."
                };
            }

            if (storedVerificationCode == verificationCodeDto.VerificationCode)
            {
                return new EmailVerificationResult
                {
                    IsSuccessful = true,
                    Message = "Email Verification successful"
                };
            }

            return new EmailVerificationResult
            {
                IsSuccessful = false,
                Message = "Incorrect verification code entered."
            };
        }
    }
}
