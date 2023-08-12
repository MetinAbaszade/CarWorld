using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;
using AspReactTestApp.Services.AutoSalonService;

namespace AspReactTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AutoSalonController : ControllerBase
    {
        private readonly IAutoSalonService _autoSalonService;

        public AutoSalonController(IAutoSalonService autoSalonService)
        {
            _autoSalonService = autoSalonService;
        }

        [HttpPost("addautosalon")]
        public async Task<ActionResult<ResponseDto>> AddAutoSalon(AutoSalon autoSalon)
        {
            var result = await _autoSalonService.AddAutoSalon(autoSalon);
            return Ok(result);
        }

        [HttpDelete("deleteautosalon")]
        public async Task<ActionResult<ResponseDto>> DeleteAutoSalon(int id)
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
}
