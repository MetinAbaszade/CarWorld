using AspReactTestApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.Entities.Concrete.CarRelated;
using AspReactTestApp.Services.TransmissionService;

namespace AspReactTestApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TransmissionController(ITransmissionService transmissionService) : ControllerBase
{
    private readonly ITransmissionService _transmissionService = transmissionService;

    [HttpPost("addtransmission")]
    public async Task<ActionResult<ResponseDTO>> AddTransmission(Transmission transmission)
    {
        var result = await _transmissionService.AddTransmission(transmission);
        return Ok(result);
    }

    [HttpDelete("deletetransmission")]
    public async Task<ActionResult<ResponseDTO>> DeleteTransmission(int id)
    {
        var result = await _transmissionService.RemoveTransmissionById(id);
        return Ok(result);
    }

    [HttpGet("gettransmissions/{languageId}")]
    public async Task<ActionResult<List<GenericEntityDTO>>> GetTransmissions(int languageId)
    {
        var transmissionList = await _transmissionService.GetAllTransmissions(languageId);
        return transmissionList;
    }
}
