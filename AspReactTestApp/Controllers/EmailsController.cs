using AspReactTestApp.DTOs; 
using Microsoft.AspNetCore.Mvc;
using AspReactTestApp.Services.EmailService; 

namespace AspReactTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailsController(IEmailService emailService)
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
