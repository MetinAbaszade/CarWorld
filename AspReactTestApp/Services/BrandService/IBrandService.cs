using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.BrandService
{
    public interface IBrandService
    {
        public Task<ResponseDto> AddBrand(Brand brand);
        public Task<ResponseDto> RemoveBrandById(int Id);
        public Task<List<Brand>> GetAllBrands();
    }
}
