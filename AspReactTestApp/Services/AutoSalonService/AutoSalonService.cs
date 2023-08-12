using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;
using Microsoft.EntityFrameworkCore;

namespace AspReactTestApp.Services.AutoSalonService
{
    public class AutoSalonService : IAutoSalonService
    {
        private readonly IAutoSalonRepository _autoSalonRepository;

        public AutoSalonService(IAutoSalonRepository autoSalonRepository)
        {
            _autoSalonRepository = autoSalonRepository;
        }

        public async Task<ResponseDto> AddAutoSalon(AutoSalon autoSalon)
        {
            ResponseDto responseDto = new();
            try
            {
                await _autoSalonRepository.Add(autoSalon);
                responseDto.IsSuccessfull = true;
                responseDto.Message = "AutoSalon Added Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<ResponseDto> RemoveAutoSalonById(int id)
        {
            ResponseDto responseDto = new();
            try
            {
                var autoSalon = await _autoSalonRepository.Get(s => s.Id == id);
                if (autoSalon != null)
                {
                    await _autoSalonRepository.Delete(autoSalon);
                    responseDto.Message = "AutoSalon Not Found!";
                    return responseDto;
                }
                responseDto.IsSuccessfull = true;
                responseDto.Message = "AutoSalon Removed Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<List<AutoSalon>> GetAllAutoSalons(int languageId)
        {
            try
            {
                var autoSalons = await _autoSalonRepository.GetList(
                     filter: s => s.AutoSalonLocales.Any(sl => sl.LanguageId == languageId),
                     orderBy: null,
                     include: source => source.Include(s => s.AutoSalonLocales));
                return autoSalons;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<AutoSalon>> GetAutoSalonsWithPagination(int pageNumber, int pageSize)
        {
            try
            {
                var autoSalons = await _autoSalonRepository.GetListWithPagination(pageNumber, pageSize);
                return autoSalons;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
