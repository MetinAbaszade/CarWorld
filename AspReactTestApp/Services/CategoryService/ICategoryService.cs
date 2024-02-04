using AspReactTestApp.DTO;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.CategoryService;

public interface ICategoryService
{
    public Task<ResponseDTO> AddCategory(Category category);
    public Task<ResponseDTO> RemoveCategoryById(int Id);
    public Task<List<GenericEntityDTO>> GetAllCategories(int languageId);
}
