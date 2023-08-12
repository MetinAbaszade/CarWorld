using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Dto;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AspReactTestApp.Services.FueltypeService
{
    public class FuelTypeService : IFuelTypeService
    {
        private readonly IFuelTypeRepository _fuelTypeRepository;
        private readonly IMapper _mapper;

        public FuelTypeService(IFuelTypeRepository fuelTypeRepository, IMapper mapper)
        {
            _fuelTypeRepository = fuelTypeRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> AddFuelType(FuelType fuelType)
        {
            ResponseDto responseDto = new();
            try
            {
                await _fuelTypeRepository.Add(fuelType);
                responseDto.IsSuccessfull = true;
                responseDto.Message = "FuelType Added Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<ResponseDto> RemoveFuelTypeById(int id)
        {
            ResponseDto responseDto = new();
            try
            {
                var fuelType = await _fuelTypeRepository.Get(ft => ft.Id == id);
                if (fuelType != null)
                {
                    await _fuelTypeRepository.Delete(fuelType);
                    responseDto.Message = "FuelType Not Found!";
                    return responseDto;
                }
                responseDto.IsSuccessfull = true;
                responseDto.Message = "FuelType Removed Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<List<GenericEntityDto>> GetAllFuelTypes(int languageId)
        {
            try
            {
                var furlTypeDtos = await _fuelTypeRepository.Select(
                    select: ft => new GenericEntityDto
                    {
                        Id = ft.Id,
                        Name = ft.FuelTypeLocales.SingleOrDefault(ftl => ftl.LanguageId == languageId).Name
                    });

                return furlTypeDtos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
