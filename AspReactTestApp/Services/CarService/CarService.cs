using AutoMapper;
using AspReactTestApp.Dto;
using AspReactTestApp.DTOs;
using Microsoft.EntityFrameworkCore;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Entities.Concrete.CarRelated;

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
                CarDetailsDto? carDetailsDto = await _carDal.SelectSingle(filter: c => c.Id == id,
                    select: c => new CarDetailsDto
                    {
                        Price = c.Price,
                        Mileage = c.Mileage,
                        Brand = c.Brand.Name,
                        Model = c.Model.Name,
                        Color = c.Color.ColorLocales.FirstOrDefault().Name,
                        Market = c.Market.MarketLocales.FirstOrDefault().Name,
                        Region = c.Region.RegionLocales.FirstOrDefault().Name,
                        Category = c.Category.CategoryLocales.FirstOrDefault().Name,
                        FuelType = c.Fueltype.FuelTypeLocales.FirstOrDefault().Name,
                        GearType = c.GearType.GearTypeLocales.FirstOrDefault().Name,
                        Currency = c.Currency.Name,
                        ReleaseYear = c.Year.Value,
                        OwnerName = c.Owner.Name,
                        HorsePower = c.HorsePower,
                        SeatCount = c.SeatCount,
                        OwnerNumber = c.Owner.Number,
                        MileageType = c.MileageType.Name,
                        Description = c.Description,
                        EngineVolume = c.EngineVolume,
                        CreditAvailable = c.CreditAvailable,
                        BarterAvailable = c.BarterAvailable,
                        Images = c.Images.Select(i => i.Url).ToList(),
                        Features = c.Features.Select(f => f.FeatureLocales.FirstOrDefault().Name).ToList(),
                        AutoSalonTitle = c.AutoSalon.Title,
                        AutoSalonNumber = c.AutoSalon.Number,
                        AutoSalonLogoUrl = c.AutoSalon.LogoUrl,
                        AutoSalonCoverUrl = c.AutoSalon.CoverUrl,
                        AutoSalonLocationUrl = c.AutoSalon.LocationUrl,
                        AutoSalonSlogan = c.AutoSalon.AutoSalonLocales.FirstOrDefault().Slogan,
                        AutoSalonDescription = c.AutoSalon.AutoSalonLocales.FirstOrDefault().Description,
                        AutoSalonLocation = c.AutoSalon.AutoSalonLocales.FirstOrDefault().Location
                    });
                return carDetailsDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<CarDto>> GetCarsWithPagination(GetCarsRequestDto getCarsRequestDto)
        {
            Func<IQueryable<Car>, IOrderedQueryable<Car>>? orderBy = null;

            if (!string.IsNullOrEmpty(getCarsRequestDto.Sort))
            {
                switch (getCarsRequestDto.Sort)
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
                    await _carDal.GetListWithPagination(getCarsRequestDto.PageNumber,
                                                        getCarsRequestDto.PageSize,
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
