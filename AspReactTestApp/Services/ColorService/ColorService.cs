using AspReactTestApp.DTO;
using AspReactTestApp.Data.DataAccess.Abstract;

namespace AspReactTestApp.Services.ColorService;

public class ColorService(IColorRepository colorRepository) : IColorService
{
    private readonly IColorRepository _colorRepository = colorRepository;

    public async Task<ResponseDTO> AddColor(Entities.Concrete.CarRelated.Color color)
    {
        ResponseDTO responseDto = new();
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

    public async Task<ResponseDTO> RemoveColorById(int id)
    {
        ResponseDTO responseDto = new();
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

    public async Task<List<GenericEntityDTO>> GetAllColors(int languageId)
    {
        try
        {
            var colorDtos = await _colorRepository.Select(
                select: c => new GenericEntityDTO
                {
                    Id = c.Id,
                    Name = c.ColorLocales.SingleOrDefault(cl => cl.LanguageId == languageId).Name
                });

            return colorDtos;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
