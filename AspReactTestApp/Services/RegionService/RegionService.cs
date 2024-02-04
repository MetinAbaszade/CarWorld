using AspReactTestApp.DTO;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.RegionService;

public class RegionService(IRegionRepository regionRepository) : IRegionService
{
    private readonly IRegionRepository _regionRepository = regionRepository;

    public async Task<ResponseDTO> AddRegion(Region region)
    {
        ResponseDTO responseDTO = new();
        try
        {
            await _regionRepository.Add(region);
            responseDTO.IsSuccessfull = true;
            responseDTO.Message = "Region Added Successfully!";
        }
        catch (Exception ex)
        {
            responseDTO.Message = ex.Message;
        }
        return responseDTO;
    }

    public async Task<ResponseDTO> RemoveRegionById(int id)
    {
        ResponseDTO responseDTO = new();
        try
        {
            var region = await _regionRepository.Get(r => r.Id == id);
            if (region != null)
            {
                await _regionRepository.Delete(region);
                responseDTO.Message = "Region Not Found!";
                return responseDTO;
            }
            responseDTO.IsSuccessfull = true;
            responseDTO.Message = "Region Removed Successfully!";
        }
        catch (Exception ex)
        {
            responseDTO.Message = ex.Message;
        }
        return responseDTO;
    }

    public async Task<List<GenericEntityDTO>> GetAllRegions(int languageId)
    {
        try
        {
            var regionDTOs = await _regionRepository.Select(
                select: r => new GenericEntityDTO
                {
                    Id = r.Id,
                    Name = r.RegionLocales.SingleOrDefault(rl => rl.LanguageId == languageId).Name
                });

            return regionDTOs;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
