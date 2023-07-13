using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.FeatureService
{
    public interface IFeatureService
    {
        public Task<ResponseDto> AddFeature(Feature feature);
        public Task<ResponseDto> RemoveFeatureById(int Id);
        public Task<List<Feature>> GetAllFeatures(string language = "Az");
    }
}
