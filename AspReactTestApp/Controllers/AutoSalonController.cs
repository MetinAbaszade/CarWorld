using AspReactTestApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.Services.AutoSalonService;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AutoSalonController(IAutoSalonService autoSalonService) : ControllerBase
{
    private readonly IAutoSalonService _autoSalonService = autoSalonService;

    [HttpPost("addautosalon")]
    public async Task<ActionResult<ResponseDTO>> AddAutoSalon(AutoSalon autoSalon)
    {
        var result = await _autoSalonService.AddAutoSalon(autoSalon);
        return Ok(result);
    }

    [HttpDelete("deleteautosalon")]
    public async Task<ActionResult<ResponseDTO>> DeleteAutoSalon(int id)
    {
        var result = await _autoSalonService.RemoveAutoSalonById(id);
        return Ok(result);
    }

    [HttpGet("getautosalons/{languageId}")]
    public async Task<ActionResult<List<AutoSalon>>> GetAutoSalons(int languageId)
    {
        var autosalonList = await _autoSalonService.GetAllAutoSalons(languageId);
        return autosalonList;
    }
}
