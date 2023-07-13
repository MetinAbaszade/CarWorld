using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.YearService
{
    public class YearService : IYearService
    {
        private readonly IYearRepository _yearRepository;

        public YearService(IYearRepository yearRepository)
        {
            _yearRepository = yearRepository;
        }

        public async Task<ResponseDto> AddYear(Year year)
        {
            ResponseDto responseDto = new();
            try
            {
                await _yearRepository.Add(year);
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Year Added Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<ResponseDto> RemoveYearById(int id)
        {
            ResponseDto responseDto = new();
            try
            {
                var year = await _yearRepository.Get(y => y.Id == id);
                if (year != null)
                {
                    await _yearRepository.Delete(year);
                    responseDto.Message = "Year Not Found!";
                    return responseDto;
                }
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Year Removed Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<List<Year>> GetAllYears()
        {
            try
            {
                var years = await _yearRepository.GetList();
                return years;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
