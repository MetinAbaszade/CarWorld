using AspReactTestApp.DTO;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.ModelService;

public interface IModelService
{
    public Task<ResponseDTO> AddModel(Model model);
    public Task<ResponseDTO> RemoveModelById(int Id);
    public Task<List<GenericEntityDTO>> GetModelsByBrandId(int brandId);
}
