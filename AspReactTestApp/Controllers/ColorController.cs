using AspReactTestApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.Services.ColorService;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ColorController(IColorService colorService) : ControllerBase
{
    private readonly IColorService _colorService = colorService;

    [HttpPost("addcolor")]
    public async Task<ActionResult<ResponseDTO>> AddColor(Color color)
    {
        var result = await _colorService.AddColor(color);
        return Ok(result);
    }

    [HttpDelete("deletecolor")]
    public async Task<ActionResult<ResponseDTO>> DeleteColor(int id)
    {
        var result = await _colorService.RemoveColorById(id);
        return Ok(result);
    }

    [HttpGet("getcolors/{languageId}")]
    public async Task<ActionResult<List<GenericEntityDTO>>> GetColors(int languageId)
    {
        var colorsList = await _colorService.GetAllColors(languageId);
        return colorsList;
    }
}
