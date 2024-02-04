using AspReactTestApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.Services.FueltypeService;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FuelTypeController(IFuelTypeService fuelTypeService) : ControllerBase
{
    private readonly IFuelTypeService _fuelTypeService = fuelTypeService;

    [HttpPost("addfueltype")]
    public async Task<ActionResult<ResponseDTO>> AddFuelType(FuelType fuelType)
    {
        var result = await _fuelTypeService.AddFuelType(fuelType);
        return Ok(result);
    }

    [HttpDelete("deletefueltype")]
    public async Task<ActionResult<ResponseDTO>> DeleteFuelType(int id)
    {
        var result = await _fuelTypeService.RemoveFuelTypeById(id);
        return Ok(result);
    }

    [HttpGet("getfueltypes/{languageId}")]
    public async Task<ActionResult<List<GenericEntityDTO>>> GetFuelTypes(int languageId)
    {
        var fuelTypeList = await _fuelTypeService.GetAllFuelTypes(languageId);
        return fuelTypeList;
    }
}
