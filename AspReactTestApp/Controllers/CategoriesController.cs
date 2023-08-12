using AspReactTestApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.Services.CategoryService; 
using AspReactTestApp.Entities.Concrete.CarRelated;
using AspReactTestApp.Dto;

namespace AspReactTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("addcategory")]
        public async Task<ActionResult<ResponseDto>> AddCategory(Category category)
        {
            var result = await _categoryService.AddCategory(category);
            return Ok(result);
        }

        [HttpDelete("deletecategory")]
        public async Task<ActionResult<ResponseDto>> DeleteCategory(int id)
        {
            var result = await _categoryService.RemoveCategoryById(id);
            return Ok(result);
        }

        [HttpGet("getcategories/{languageId}")]
        public async Task<ActionResult<List<GenericEntityDto>>> GetCategories(int languageId)
        {
            var categoriesList = await _categoryService.GetAllCategories(languageId);
            return categoriesList;
        }
    }
}
