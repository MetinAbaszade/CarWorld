using AutoMapper;
using AspReactTestApp.DTO;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.FueltypeService;

public class FuelTypeService(IFuelTypeRepository fuelTypeRepository) : IFuelTypeService
{
    private readonly IFuelTypeRepository _fuelTypeRepository = fuelTypeRepository;

    public async Task<ResponseDTO> AddFuelType(FuelType fuelType)
    {
        ResponseDTO responseDTO = new();
        try
        {
            await _fuelTypeRepository.Add(fuelType);
            responseDTO.IsSuccessfull = true;
            responseDTO.Message = "FuelType Added Successfully!";
        }
        catch (Exception ex)
        {
            responseDTO.Message = ex.Message;
        }
        return responseDTO;
    }

    public async Task<ResponseDTO> RemoveFuelTypeById(int id)
    {
        ResponseDTO responseDTO = new();
        try
        {
            var fuelType = await _fuelTypeRepository.Get(ft => ft.Id == id);
            if (fuelType != null)
            {
                await _fuelTypeRepository.Delete(fuelType);
                responseDTO.Message = "FuelType Not Found!";
                return responseDTO;
            }
            responseDTO.IsSuccessfull = true;
            responseDTO.Message = "FuelType Removed Successfully!";
        }
        catch (Exception ex)
        {
            responseDTO.Message = ex.Message;
        }
        return responseDTO;
    }

    public async Task<List<GenericEntityDTO>> GetAllFuelTypes(int languageId)
    {
        try
        {
            var furlTypeDTOs = await _fuelTypeRepository.Select(
                select: ft => new GenericEntityDTO
                {
                    Id = ft.Id,
                    Name = ft.FuelTypeLocales.SingleOrDefault(ftl => ftl.LanguageId == languageId).Name
                });

            return furlTypeDTOs;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
