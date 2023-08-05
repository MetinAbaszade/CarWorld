using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Data.DataAccess.Concrete.EntityFramework;
using AspReactTestApp.Dto;
using AspReactTestApp.DTOs;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace AspReactTestApp.Services.ColorService
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
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

        public async Task<List<GenericEntityDto>> GetAllColors(string language)
        {
            try
            {
                var colors = await _colorRepository.GetList(
                    filter: c => c.ColorLocales.Any(cl => cl.Language.DisplayName == language),
                    orderBy: null,
                    include: source => source.Include(c => c.ColorLocales));

                List<GenericEntityDto> colorDtos = _mapper.Map<List<GenericEntityDto>>(colors);
                return colorDtos;
            }
            catch (Exception)
            {
                throw;
            } 
        }
    }
}
