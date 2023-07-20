using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.RegionService
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;

        public RegionService(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<ResponseDto> AddRegion(Region region)
        {
            ResponseDto responseDto = new();
            try
            {
                await _regionRepository.Add(region);
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Region Added Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<ResponseDto> RemoveRegionById(int id)
        {
            ResponseDto responseDto = new();
            try
            {
                var region = await _regionRepository.Get(r => r.Id == id);
                if (region != null)
                {
                    await _regionRepository.Delete(region);
                    responseDto.Message = "Region Not Found!";
                    return responseDto;
                }
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Region Removed Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<List<Region>> GetAllRegions(string language)
        {
            try
            {
                var regions = await _regionRepository.GetList(
                    filter: c => c.RegionLocales.Any(fl => fl.Language.DisplayName == language),
                    orderBy: null,
                    includeProperties: "RegionLocales");
                return regions;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
