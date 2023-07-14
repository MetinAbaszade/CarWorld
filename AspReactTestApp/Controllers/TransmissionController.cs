using AspReactTestApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.Entities.Concrete.CarRelated;
using AspReactTestApp.Services.TransmissionService;

namespace AspReactTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransmissionController : ControllerBase
    {
        private readonly ITransmissionService _transmissionService;

        public TransmissionController(ITransmissionService transmissionService)
        {
            _transmissionService = transmissionService;
        }

        [HttpPost("addtransmission")]
        public async Task<ActionResult<ResponseDto>> AddTransmission(Transmission transmission)
        {
            var result = await _transmissionService.AddTransmission(transmission);
            return Ok(result);
        }

        [HttpDelete("deletetransmission")]
        public async Task<ActionResult<ResponseDto>> DeleteTransmission(int id)
        {
            var result = await _transmissionService.RemoveTransmissionById(id);
            return Ok(result);
        }

        [HttpGet("gettransmissions")]
        public async Task<ActionResult<List<Transmission>>> GetTransmissions()
        {
            var transmissionList = await _transmissionService.GetAllTransmissions();
            return transmissionList;
        }
    }
}
