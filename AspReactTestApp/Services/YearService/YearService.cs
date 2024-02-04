using AspReactTestApp.DTO;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.YearService;

public class YearService(IYearRepository yearRepository) : IYearService
{
    private readonly IYearRepository _yearRepository = yearRepository;

    public async Task<ResponseDTO> AddYear(Year year)
    {
        ResponseDTO responseDTO = new();
        try
        {
            await _yearRepository.Add(year);
            responseDTO.IsSuccessfull = true;
            responseDTO.Message = "Year Added Successfully!";
        }
        catch (Exception ex)
        {
            responseDTO.Message = ex.Message;
        }
        return responseDTO;
    }

    public async Task<ResponseDTO> RemoveYearById(int id)
    {
        ResponseDTO responseDTO = new();
        try
        {
            var year = await _yearRepository.Get(y => y.Id == id);
            if (year != null)
            {
                await _yearRepository.Delete(year);
                responseDTO.Message = "Year Not Found!";
                return responseDTO;
            }
            responseDTO.IsSuccessfull = true;
            responseDTO.Message = "Year Removed Successfully!";
        }
        catch (Exception ex)
        {
            responseDTO.Message = ex.Message;
        }
        return responseDTO;
    }

    public async Task<List<Year>> GetAllYears()
    {
        try
        {
            var years = await _yearRepository.GetList();
            return years;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
