using AspReactTestApp.DTO;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.BrandService;

public interface IBrandService
{
    public Task<ResponseDTO> AddBrand(Brand brand);
    public Task<ResponseDTO> RemoveBrandById(int Id);
    public Task<List<Brand>> GetAllBrands();
}
