using AspReactTestApp.Dto;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.ColorService
{
    public interface IColorService
    {
        public Task<ResponseDto> AddColor(Color color);
        public Task<ResponseDto> RemoveColorById(int Id);
        public Task<List<GenericEntityDto>> GetAllColors(string language = "Az");
    }
}
