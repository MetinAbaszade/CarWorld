using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.CarService
{
    public interface ICarService
    {
        public Task<ResponseDto> AddCar(Car car);
        public Task<ResponseDto> RemoveCar(int id);
        public Task<Car> GetCarDetails(int id);
        public Task<List<Car>> GetCarsWithPagination(int pageNumber, int pageSize = 20, string sort = "");
    }
}
