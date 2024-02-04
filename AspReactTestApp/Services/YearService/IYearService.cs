using AspReactTestApp.DTO;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.YearService;

public interface IYearService
{
    public Task<ResponseDTO> AddYear(Year year);
    public Task<ResponseDTO> RemoveYearById(int Id);
    public Task<List<Year>> GetAllYears();
}
