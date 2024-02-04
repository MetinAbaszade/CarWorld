using AspReactTestApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.Services.ModelService;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ModelController(IModelService modelService) : ControllerBase
{
    private readonly IModelService _modelService = modelService;

    [HttpPost("addmodel")]
    public async Task<ActionResult<ResponseDTO>> AddModel(Model model)
    {
        var result = await _modelService.AddModel(model);
        return Ok(result);
    }

    [HttpDelete("deletemodel")]
    public async Task<ActionResult<ResponseDTO>> DeleteModel(int id)
    {
        var result = await _modelService.RemoveModelById(id);
        return Ok(result);
    }

    [HttpGet("getmodels/{brandId}")]
    public async Task<ActionResult<List<GenericEntityDTO>>> GetModels(int brandId)
    {
        var modelList = await _modelService.GetModelsByBrandId(brandId);
        return modelList;
    }
}
