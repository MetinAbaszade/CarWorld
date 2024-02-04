using AspReactTestApp.DTO;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.ColorService;

public interface IColorService
{
    public Task<ResponseDTO> AddColor(Color color);
    public Task<ResponseDTO> RemoveColorById(int Id);
    public Task<List<GenericEntityDTO>> GetAllColors(int languageId);
}
