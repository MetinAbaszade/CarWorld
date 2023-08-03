using AspReactTestApp.DTOs;
using Microsoft.EntityFrameworkCore;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Entities.Concrete.CarRelated;
using AspReactTestApp.Dto;
using AutoMapper;

namespace AspReactTestApp.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> AddCategory(Category category)
        {
            ResponseDto responseDto = new();
            try
            {
                await _categoryRepository.Add(category);
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Category Added Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<ResponseDto> RemoveCategoryById(int id)
        {
            ResponseDto responseDto = new();
            try
            {
                var category = await _categoryRepository.Get(c => c.Id == id);
                if (category != null)
                {
                    await _categoryRepository.Delete(category);
                    responseDto.Message = "Category Not Found!";
                    return responseDto;
                }
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Category Removed Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<List<GenericEntityDto>> GetAllCategories(string language)
        {
            try
            {
                var categories = await _categoryRepository.GetList(
                    filter: c => c.CategoryLocales.Any(cl => cl.Language.LanguageName == language),
                    orderBy: null,
                    include: source => source.Include(c => c.CategoryLocales));

                List<GenericEntityDto> categoryDtos = _mapper.Map<List<GenericEntityDto>>(categories);
                return categoryDtos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
