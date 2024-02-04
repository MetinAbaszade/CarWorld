using AutoMapper;
using AspReactTestApp.DTO;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.CategoryService;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<ResponseDTO> AddCategory(Category category)
    {
        ResponseDTO responseDTO = new();
        try
        {
            await _categoryRepository.Add(category);
            responseDTO.IsSuccessfull = true;
            responseDTO.Message = "Category Added Successfully!";
        }
        catch (Exception ex)
        {
            responseDTO.Message = ex.Message;
        }
        return responseDTO;
    }

    public async Task<ResponseDTO> RemoveCategoryById(int id)
    {
        ResponseDTO responseDTO = new();
        try
        {
            var category = await _categoryRepository.Get(c => c.Id == id);
            if (category != null)
            {
                await _categoryRepository.Delete(category);
                responseDTO.Message = "Category Not Found!";
                return responseDTO;
            }
            responseDTO.IsSuccessfull = true;
            responseDTO.Message = "Category Removed Successfully!";
        }
        catch (Exception ex)
        {
            responseDTO.Message = ex.Message;
        }
        return responseDTO;
    }

    public async Task<List<GenericEntityDTO>> GetAllCategories(int languageId)
    {
        try
        {
            var categoryDTOs = await _categoryRepository.Select(
                select: c => new GenericEntityDTO
                {
                    Id = c.Id,
                    Name = c.CategoryLocales.SingleOrDefault(cl => cl.LanguageId == languageId).Name
                });

           return categoryDTOs;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
