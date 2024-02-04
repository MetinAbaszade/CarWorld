using AspReactTestApp.DTO;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.AutoSalonService;

public interface IAutoSalonService
{
    public Task<ResponseDTO> AddAutoSalon(AutoSalon autoSalon);
    public Task<ResponseDTO> RemoveAutoSalonById(int Id);
    public Task<List<AutoSalon>> GetAllAutoSalons(int languageId);
    public Task<List<AutoSalon>> GetAutoSalonsWithPagination(int pageNumber = 1, int pageSize = 20);
}
