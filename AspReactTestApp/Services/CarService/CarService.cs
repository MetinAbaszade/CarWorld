using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Dto;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AspReactTestApp.Services.CarService
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carDal;
        private readonly IMapper _mapper;

        public CarService(ICarRepository carDal, IMapper mapper)
        {
            _carDal = carDal;
            _mapper = mapper;
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

        public async Task<CarDetailsDto> GetCarDetails(int id)
        {
            try
            {
                Car car = await _carDal.Get(filter: c => c.Id == id,
                    include: source => source.Include((c) => c.Region).ThenInclude(r => r.RegionLocales)
                                     .Include((c) => c.Year)
                                     .Include((c) => c.Owner)
                                     .Include((c) => c.Brand)
                                     .Include((c) => c.Model)
                                     .Include((c) => c.Images)
                                     .Include((c) => c.Currency)
                                     .Include((c) => c.AutoSalon)
                                     .Include((c) => c.MileageType)
                                     .Include((c) => c.Color).ThenInclude(c => c.ColorLocales)
                                     .Include((c) => c.Market).ThenInclude(m => m.MarketLocales)
                                     .Include((c) => c.Category).ThenInclude(c => c.CategoryLocales)
                                     .Include((c) => c.Fueltype).ThenInclude(ft => ft.FuelTypeLocales)
                                     .Include((c) => c.GearType).ThenInclude(gt => gt.GearTypeLocales)
                                     .Include((c) => c.Features).ThenInclude(f => f.FeatureLocales)
                                     .Include((c) => c.Transmission).ThenInclude(t => t.TransmissionLocales));
                CarDetailsDto carDetailsDto = _mapper.Map<Car, CarDetailsDto>(car);
                return carDetailsDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<CarDto>> GetCarsWithPagination(string sort, int pageNumber = 1, int pageSize = 5)
        {
            Func<IQueryable<Car>, IOrderedQueryable<Car>> orderBy = null;

            if (!string.IsNullOrEmpty(sort))
            {
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
            }

            try
            {
                List<Car> cars =
                    await _carDal.GetListWithPagination(pageNumber,
                                                        pageSize,
                                                        filter: null,
                                                        orderBy,
                                                        include: source => source.Include((c) => c.Brand)
                                                                                 .Include((c) => c.Year)
                                                                                 .Include((c) => c.Model)
                                                                                 .Include((c) => c.Images)
                                                                                 .Include((c) => c.Currency)
                                                                                 .Include((c) => c.MileageType)
                                                                                 .Include((c) => c.Region).ThenInclude(r => r.RegionLocales));
                List<CarDto> carDtos = _mapper.Map<List<Car>, List<CarDto>>(cars);
                return carDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
