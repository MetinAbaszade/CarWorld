using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.CategoryService
{
    public interface ICategoryService
    {
        public Task<ResponseDto> AddCategory(Category category);
        public Task<ResponseDto> RemoveCategoryById(int Id);
        public Task<List<Category>> GetAllCategories(string language = "Az");
    }
}
