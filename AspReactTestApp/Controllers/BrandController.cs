using AspReactTestApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.Services.BrandService;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BrandController(IBrandService brandService) : ControllerBase
{
    private readonly IBrandService _brandService = brandService;

    [HttpPost("addbrand")]
    public async Task<ActionResult<ResponseDTO>> AddBrand(Brand brand)
    {
        var result = await _brandService.AddBrand(brand);
        return Ok(result);
    }

    [HttpDelete("deletebrand")]
    public async Task<ActionResult<ResponseDTO>> DeleteBrand(int id)
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
