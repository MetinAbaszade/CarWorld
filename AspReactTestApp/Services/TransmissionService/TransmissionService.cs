using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.TransmissionService
{
    public class TransmissionService : ITransmissionService
    {
        private readonly ITransmissionRepository _transmissionRepository;

        public TransmissionService(ITransmissionRepository transmissionRepository)
        {
            _transmissionRepository = transmissionRepository;
        }

        public async Task<ResponseDto> AddTransmission(Transmission transmission)
        {
            ResponseDto responseDto = new();
            try
            {
                await _transmissionRepository.Add(transmission);
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Transmission Added Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<ResponseDto> RemoveTransmissionById(int id)
        {
            ResponseDto responseDto = new();
            try
            {
                var transmission = await _transmissionRepository.Get(t => t.Id == id);
                if (transmission != null)
                {
                    await _transmissionRepository.Delete(transmission);
                    responseDto.Message = "Transmission Not Found!";
                    return responseDto;
                }
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Transmission Removed Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<List<Transmission>> GetAllTransmissions(string language)
        {
            try
            {
                var transmissions = await _transmissionRepository.GetList(
                    filter: t => t.TransmissionLocales.Any(tl => tl.Language.LanguageName == language),
                    orderBy: null,
                    includeProperties: "TransmissionLocales");
                return transmissions;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
