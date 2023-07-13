using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.FueltypeService
{
    public class FueltypeService
    {
        private readonly IFueltypeRepository _fueltypeRepository;

        public FueltypeService(IFueltypeRepository fueltypeRepository)
        {
            _fueltypeRepository = fueltypeRepository;
        }

        public async Task<ResponseDto> AddFueltype(Fueltype fueltype)
        {
            ResponseDto responseDto = new();
            try
            {
                await _fueltypeRepository.Add(fueltype);
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Fueltype Added Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<ResponseDto> RemoveFueltypeById(int id)
        {
            ResponseDto responseDto = new();
            try
            {
                var feature = await _fueltypeRepository.Get(ft => ft.Id == id);
                if (feature != null)
                {
                    await _fueltypeRepository.Delete(feature);
                    responseDto.Message = "Fueltype Not Found!";
                    return responseDto;
                }
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Fueltype Removed Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<List<Fueltype>> GetAllFueltypes(string language)
        {
            try
            {
                var fueltypes = await _fueltypeRepository.GetList(
                    filter: c => c.FueltypeLocales.Any(fl => fl.Language.LanguageName == language),
                    orderBy: null,
                    includeProperties: "FueltypeLocales");
                return fueltypes;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
