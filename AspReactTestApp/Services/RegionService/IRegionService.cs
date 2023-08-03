using AspReactTestApp.Dto;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.RegionService
{
    public interface IRegionService
    {
        public Task<ResponseDto> AddRegion(Region region);
        public Task<ResponseDto> RemoveRegionById(int Id);
        public Task<List<GenericEntityDto>> GetAllRegions(string language = "Az");
    }
}
