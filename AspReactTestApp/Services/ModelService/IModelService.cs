using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.ModelService
{
    public interface IModelService
    {
        public Task<ResponseDto> AddModel(Model model);
        public Task<ResponseDto> RemoveModelById(int Id);
        public Task<List<Model>> GetModelsByBrandId(int brandId);
    }
}
