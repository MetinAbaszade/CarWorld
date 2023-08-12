using AspReactTestApp.Dto;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;
using AspReactTestApp.Services.CategoryService;
using AspReactTestApp.Services.ColorService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspReactTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpPost("addcolor")]
        public async Task<ActionResult<ResponseDto>> AddColor(Color color)
        {
            var result = await _colorService.AddColor(color);
            return Ok(result);
        }

        [HttpDelete("deletecolor")]
        public async Task<ActionResult<ResponseDto>> DeleteColor(int id)
        {
            var result = await _colorService.RemoveColorById(id);
            return Ok(result);
        }

        [HttpGet("getcolors/{languageId}")]
        public async Task<ActionResult<List<GenericEntityDto>>> GetColors(int languageId)
        {
            var colorsList = await _colorService.GetAllColors(languageId);
            return colorsList;
        }
    }
}
