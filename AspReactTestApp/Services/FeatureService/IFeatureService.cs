using AspReactTestApp.DTO;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.FeatureService;

public interface IFeatureService
{
    public Task<ResponseDTO> AddFeature(Feature feature);
    public Task<ResponseDTO> RemoveFeatureById(int Id);
    public Task<List<GenericEntityDTO>> GetAllFeatures(int languageId);
}
