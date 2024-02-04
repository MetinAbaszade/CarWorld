using AspReactTestApp.DTO;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.BrandService;

public class BrandService(IBrandRepository brandRepository) : IBrandService
{
    private readonly IBrandRepository _brandRepository = brandRepository;

    public async Task<ResponseDTO> AddBrand(Brand brand)
    {
        ResponseDTO responseDTO = new();
        try
        {
            await _brandRepository.Add(brand);
            responseDTO.IsSuccessfull = true;
            responseDTO.Message = "Brand Added Successfully!";
        }
        catch (Exception ex)
        {
            responseDTO.Message = ex.Message;
        }
        return responseDTO;
    }

    public async Task<ResponseDTO> RemoveBrandById(int id)
    {
        ResponseDTO responseDTO = new();
        try
        {
            var brand = await _brandRepository.Get(b => b.Id == id);
            if (brand != null)
            {
                await _brandRepository.Delete(brand);
                responseDTO.Message = "Brand Not Found!";
                return responseDTO;
            }
            responseDTO.IsSuccessfull = true;
            responseDTO.Message = "Brand Removed Successfully!";
        }
        catch (Exception ex)
        {
            responseDTO.Message = ex.Message;
        }
        return responseDTO;
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
