using AspReactTestApp.DTOs;
using AspReactTestApp.Services.AuthService;
using AspReactTestApp.Services.EmailService;
using AspReactTestApp.Services.UserService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspReactTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("sendverificationcode")]
        public ActionResult<ResponseDto> SendVerificationCode([FromBody] string recipientEmail)
        {
            var result = _emailService.SendVerificationCode(recipientEmail);
            if (result.IsSuccessfull)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPost("checkverificationcode")]
        public ActionResult<ResponseDto> CheckVerificationCode(CheckVerificationCodeDto verificationCodeDto)
        {
            var result = _emailService.CheckVerificationCode(verificationCodeDto);
            if (result.IsSuccessfull)
                return Ok(result);
            return NotFound(result);
        }
    }
}
