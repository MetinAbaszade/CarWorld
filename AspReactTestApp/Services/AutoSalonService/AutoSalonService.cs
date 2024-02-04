using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.DTO;
using AspReactTestApp.Entities.Concrete.CarRelated;
using Microsoft.EntityFrameworkCore;

namespace AspReactTestApp.Services.AutoSalonService;

public class AutoSalonService(IAutoSalonRepository autoSalonRepository) : IAutoSalonService
{
    private readonly IAutoSalonRepository _autoSalonRepository = autoSalonRepository;

    public async Task<ResponseDTO> AddAutoSalon(AutoSalon autoSalon)
    {
        ResponseDTO responseDTO = new();
        try
        {
            await _autoSalonRepository.Add(autoSalon);
            responseDTO.IsSuccessfull = true;
            responseDTO.Message = "AutoSalon Added Successfully!";
        }
        catch (Exception ex)
        {
            responseDTO.Message = ex.Message;
        }
        return responseDTO;
    }

    public async Task<ResponseDTO> RemoveAutoSalonById(int id)
    {
        ResponseDTO responseDTO = new();
        try
        {
            var autoSalon = await _autoSalonRepository.Get(s => s.Id == id);
            if (autoSalon != null)
            {
                await _autoSalonRepository.Delete(autoSalon);
                responseDTO.Message = "AutoSalon Not Found!";
                return responseDTO;
            }
            responseDTO.IsSuccessfull = true;
            responseDTO.Message = "AutoSalon Removed Successfully!";
        }
        catch (Exception ex)
        {
            responseDTO.Message = ex.Message;
        }
        return responseDTO;
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
