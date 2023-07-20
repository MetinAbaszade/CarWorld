using AspReactTestApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.Services.BrandService;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost("addbrand")]
        public async Task<ActionResult<ResponseDto>> AddBrand(Brand brand)
        {
            var result = await _brandService.AddBrand(brand);
            return Ok(result);
        }

        [HttpDelete("deletebrand")]
        public async Task<ActionResult<ResponseDto>> DeleteBrand(int id)
        {
            var result = await _brandService.RemoveBrandById(id);
            return Ok(result);
        }

        [HttpGet("getbrands")]
        public async Task<ActionResult<List<Brand>>> GetBrands()
        {
            var brandList = await _brandService.GetAllBrands();
            return brandList;
        }
    }
}
