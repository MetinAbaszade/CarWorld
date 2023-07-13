using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.AutoSalonService
{
    public interface IAutoSalonService
    {
        public Task<ResponseDto> AddAutoSalon(AutoSalon autoSalon);
        public Task<ResponseDto> RemoveAutoSalonById(int Id);
        public Task<List<AutoSalon>> GetAllAutoSalons(string language = "Az");
        public Task<List<AutoSalon>> GetAutoSalonsWithPagination(int pageNumber = 1, int pageSize = 20);
    }
}
