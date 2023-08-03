using AspReactTestApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.Services.FueltypeService;
using AspReactTestApp.Entities.Concrete.CarRelated;
using AspReactTestApp.Dto;

namespace AspReactTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FuelTypeController : ControllerBase
    {
        private readonly IFuelTypeService _fuelTypeService;

        public FuelTypeController(IFuelTypeService fuelTypeService)
        {
            _fuelTypeService = fuelTypeService;
        }

        [HttpPost("addfueltype")]
        public async Task<ActionResult<ResponseDto>> AddFuelType(FuelType fuelType)
        {
            var result = await _fuelTypeService.AddFuelType(fuelType);
            return Ok(result);
        }

        [HttpDelete("deletefueltype")]
        public async Task<ActionResult<ResponseDto>> DeleteFuelType(int id)
        {
            var result = await _fuelTypeService.RemoveFuelTypeById(id);
            return Ok(result);
        }

        [HttpGet("getfueltypes")]
        public async Task<ActionResult<List<GenericEntityDto>>> GetFuelTypes()
        {
            var fuelTypeList = await _fuelTypeService.GetAllFuelTypes();
            return fuelTypeList;
        }
    }
}
