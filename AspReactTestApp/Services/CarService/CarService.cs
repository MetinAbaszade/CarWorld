using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.CarService
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carDal;

        public CarService(ICarRepository carDal)
        {
            _carDal = carDal;
        }

        public async Task<ResponseDto> AddCar(Car car)
        {
            ResponseDto responseDto = new();
            try
            {
                await _carDal.Add(car);
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Car Added Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }
        public async Task<ResponseDto> RemoveCar(int id)
        {
            ResponseDto responseDto = new();
            try
            {
                var car = await _carDal.Get(c => c.Id == id);
                if (car == null)
                {
                    responseDto.Message = "Car Not found with specified id";
                    return responseDto;
                }

                await _carDal.Delete(car);
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Car Deleted Successfully";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }

            return responseDto;
        }
        public async Task<Car> GetCarDetails(int id)
        {
            try
            {
                Car car = await _carDal.Get(c => c.Id == id);
                return car;
            }
            catch (Exception)
            {
                throw;
            } 
        }
        public async Task<List<Car>> GetCarsWithPagination(int pageNumber = 1, int pageSize = 20, string sort = "")
        {
            Func<IQueryable<Car>, IOrderedQueryable<Car>> orderBy = null;

            switch (sort)
            {
                case "price_low_to_high":
                    orderBy = q => q.OrderBy(c => c.Price);
                    break;
                case "price_high_to_low":
                    orderBy = q => q.OrderByDescending(c => c.Price);
                    break;
                case "date":
                    orderBy = q => q.OrderBy(c => c.LastUpdated);
                    break;
                default:
                    orderBy = q => q.OrderBy(c => c.Id); 
                    break;
            }

            try
            {
                List<Car> cars = await _carDal.GetListWithPagination(pageNumber, pageSize, filter: null, orderBy);
                return cars;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
