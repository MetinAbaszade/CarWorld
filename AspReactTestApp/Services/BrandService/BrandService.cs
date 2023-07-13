using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.BrandService
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<ResponseDto> AddBrand(Brand brand)
        {
            ResponseDto responseDto = new();
            try
            {
                await _brandRepository.Add(brand);
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Brand Added Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<ResponseDto> RemoveBrandById(int id)
        {
            ResponseDto responseDto = new();
            try
            {
                var brand = await _brandRepository.Get(b => b.Id == id);
                if (brand != null)
                {
                    await _brandRepository.Delete(brand);
                    responseDto.Message = "Brand Not Found!";
                    return responseDto;
                }
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Brand Removed Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<List<Brand>> GetAllBrands()
        {
            try
            {
                var brands = await _brandRepository.GetList();
                return brands;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
