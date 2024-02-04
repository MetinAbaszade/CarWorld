using AspReactTestApp.DTO;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.RegionService;

public interface IRegionService
{
    public Task<ResponseDTO> AddRegion(Region region);
    public Task<ResponseDTO> RemoveRegionById(int Id);
    public Task<List<GenericEntityDTO>> GetAllRegions(int languageId);
}
