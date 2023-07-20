using AspReactTestApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.Services.FueltypeService;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FueltypeController : ControllerBase
    {
        private readonly IFueltypeService _fueltypeService;

        public FueltypeController(IFueltypeService fueltypeService)
        {
            _fueltypeService = fueltypeService;
        }

        [HttpPost("addfueltype")]
        public async Task<ActionResult<ResponseDto>> AddFuelType(Fueltype fueltype)
        {
            var result = await _fueltypeService.AddFueltype(fueltype);
            return Ok(result);
        }

        [HttpDelete("deletefueltype")]
        public async Task<ActionResult<ResponseDto>> DeleteFuelType(int id)
        {
            var result = await _fueltypeService.RemoveFueltypeById(id);
            return Ok(result);
        }

        [HttpGet("getfueltypes")]
        public async Task<ActionResult<List<Fueltype>>> GetFuelTypes()
        {
            var fueltypeList = await _fueltypeService.GetAllFueltypes();
            return fueltypeList;
        }
    }
}
