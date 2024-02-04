using AspReactTestApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.Services.RegionService;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RegionController(IRegionService regionService) : ControllerBase
{
    private readonly IRegionService _regionService = regionService;

    [HttpPost("addregion")]
    public async Task<ActionResult<ResponseDTO>> AddRegion(Region region)
    {
        var result = await _regionService.AddRegion(region);
        return Ok(result);
    }

    [HttpDelete("deleteregion")]
    public async Task<ActionResult<ResponseDTO>> DeleteRegion(int id)
    {
        var result = await _regionService.RemoveRegionById(id);
        return Ok(result);
    }

    [HttpGet("getregions/{languageId}")]
    public async Task<ActionResult<List<GenericEntityDTO>>> GetRegions(int languageId)
    {
        var regionList = await _regionService.GetAllRegions(languageId);
        return regionList;
    }
}
