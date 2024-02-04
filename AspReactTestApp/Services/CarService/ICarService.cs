using AspReactTestApp.DTO;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.CarService;

public interface ICarService
{
    public Task<ResponseDTO> AddCar(Car car);
    public Task<ResponseDTO> RemoveCar(int id);
    public Task<CarDetailsDTO> GetCarDetails(int carId, int languageId);
    public Task<List<CarDTO>> GetCarsWithPagination(GetCarsRequestDTO getCarsRequestDto);
}
