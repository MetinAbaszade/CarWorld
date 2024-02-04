using AspReactTestApp.DTO; 
using Microsoft.AspNetCore.Mvc;
using AspReactTestApp.Services.EmailService; 

namespace AspReactTestApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailsController(IEmailService emailService) : ControllerBase
{
    private readonly IEmailService _emailService = emailService;

    [HttpPost("sendverificationcode")]
    public ActionResult<ResponseDTO> SendVerificationCode([FromBody] string recipientEmail)
    {
        var result = _emailService.SendVerificationCode(recipientEmail);
        if (result.IsSuccessfull)
            return Ok(result);

        return NotFound(result);
    }

    [HttpPost("checkverificationcode")]
    public ActionResult<ResponseDTO> CheckVerificationCode(CheckVerificationCodeDTO verificationCodeDto)
    {
        var result = _emailService.CheckVerificationCode(verificationCodeDto);
        if (result.IsSuccessfull)
            return Ok(result);
        return NotFound(result);
    }
}
