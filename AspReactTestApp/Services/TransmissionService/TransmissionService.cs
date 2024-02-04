using AspReactTestApp.DTO;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.TransmissionService;

public class TransmissionService(ITransmissionRepository transmissionRepository) : ITransmissionService
{
    private readonly ITransmissionRepository _transmissionRepository = transmissionRepository;

    public async Task<ResponseDTO> AddTransmission(Transmission transmission)
    {
        ResponseDTO responseDTO = new();
        try
        {
            await _transmissionRepository.Add(transmission);
            responseDTO.IsSuccessfull = true;
            responseDTO.Message = "Transmission Added Successfully!";
        }
        catch (Exception ex)
        {
            responseDTO.Message = ex.Message;
        }
        return responseDTO;
    }

    public async Task<ResponseDTO> RemoveTransmissionById(int id)
    {
        ResponseDTO responseDTO = new();
        try
        {
            var transmission = await _transmissionRepository.Get(t => t.Id == id);
            if (transmission != null)
            {
                await _transmissionRepository.Delete(transmission);
                responseDTO.Message = "Transmission Not Found!";
                return responseDTO;
            }
            responseDTO.IsSuccessfull = true;
            responseDTO.Message = "Transmission Removed Successfully!";
        }
        catch (Exception ex)
        {
            responseDTO.Message = ex.Message;
        }
        return responseDTO;
    }

    public async Task<List<GenericEntityDTO>> GetAllTransmissions(int languageId)
    {
        try
        {
            var transmissionDTOs = await _transmissionRepository.Select(
                select: t => new GenericEntityDTO
                {
                    Id = t.Id,
                    Name = t.TransmissionLocales.SingleOrDefault(tl => tl.LanguageId == languageId).Name
                });

           return transmissionDTOs;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
