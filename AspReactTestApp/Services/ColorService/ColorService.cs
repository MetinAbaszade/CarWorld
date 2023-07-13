using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Data.DataAccess.Concrete.EntityFramework;
using AspReactTestApp.DTOs;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;
using System.Drawing;

namespace AspReactTestApp.Services.ColorService
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;

        public ColorService(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public async Task<ResponseDto> AddColor(Entities.Concrete.CarRelated.Color color)
        {
            ResponseDto responseDto = new();
            try
            {
                await _colorRepository.Add(color);
                responseDto.IsSuccessfull = true;
                responseDto.Message = "User Added Successfully!";
            }
            catch (Exception ex)
            {

                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<ResponseDto> RemoveColorById(int id)
        {
            ResponseDto responseDto = new();
            try
            {
                var color = await _colorRepository.Get(c => c.Id == id);
                if (color != null)
                {
                    await _colorRepository.Delete(color);
                    responseDto.Message = "User Not Found!";
                    return responseDto;
                }
                responseDto.IsSuccessfull = true;
                responseDto.Message = "User Removed Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<List<Entities.Concrete.CarRelated.Color>> GetAllColors(string language)
        {
            try
            {
                var colors = await _colorRepository.GetList(
                    filter: c => c.ColorLocales.Any(cl => cl.Language.LanguageName == language),
                    orderBy: null,
                    includeProperties: "ColorLocales");
                return colors;
            }
            catch (Exception ex)
            {
                throw;
            } 
        }
    }
}
