using AspReactTestApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.Services.CategoryService; 
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpPost("addcategory")]
    public async Task<ActionResult<ResponseDTO>> AddCategory(Category category)
    {
        var result = await _categoryService.AddCategory(category);
        return Ok(result);
    }

    [HttpDelete("deletecategory")]
    public async Task<ActionResult<ResponseDTO>> DeleteCategory(int id)
    {
        var result = await _categoryService.RemoveCategoryById(id);
        return Ok(result);
    }

    [HttpGet("getcategories/{languageId}")]
    public async Task<ActionResult<List<GenericEntityDTO>>> GetCategories(int languageId)
    {
        var categoriesList = await _categoryService.GetAllCategories(languageId);
        return categoriesList;
    }
}
