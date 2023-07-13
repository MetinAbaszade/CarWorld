using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.YearService
{
    public interface IYearService
    {
        public Task<ResponseDto> AddYear(Year year);
        public Task<ResponseDto> RemoveYearById(int Id);
        public Task<List<Year>> GetAllYears();
    }
}
