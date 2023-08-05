using AspReactTestApp.Dto;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.CarService
{
    public interface ICarService
    {
        public Task<ResponseDto> AddCar(Car car);
        public Task<ResponseDto> RemoveCar(int id);
        public Task<CarDetailsDto> GetCarDetails(int id);
        public Task<List<CarDto>> GetCarsWithPagination(GetCarsRequestDto getCarsRequestDto);
    }
}
