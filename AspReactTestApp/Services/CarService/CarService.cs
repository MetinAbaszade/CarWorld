using AutoMapper;
using AspReactTestApp.DTO;
using Microsoft.EntityFrameworkCore;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Entities.Concrete.CarRelated;
using TurboazFetching.Entities;

namespace AspReactTestApp.Services.CarService;

public class CarService(ICarRepository carDal, IMapper mapper) : ICarService
{
    private readonly ICarRepository _carDal = carDal;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseDTO> AddCar(Car car)
    {
        ResponseDTO responseDto = new();
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

    public async Task<ResponseDTO> RemoveCar(int id)
    {
        ResponseDTO responseDto = new();
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

    public async Task<CarDetailsDTO> GetCarDetails(int carId, int languageId)
    {
        try
        {
            #region Way1
            //CarDetailsDTO? carDetailsDto = await _carDal.SelectSingle(filter: c => c.Id == carId,
            //    select: c => new CarDetailsDTO
            //    {
            //        Price = c.Price,
            //        Mileage = c.Mileage,
            //        Brand = c.Brand.Name,
            //        Model = c.Model.Name,
            //        Color = c.Color.ColorLocales.FirstOrDefault(c => c.LanguageId == languageId).Name,
            //        Market = c.Market.MarketLocales.FirstOrDefault(c => c.LanguageId == languageId).Name,
            //        Region = c.Region.RegionLocales.FirstOrDefault(c => c.LanguageId == languageId).Name,
            //        CategoryId = c.CategoryId,
            //        Category = c.Category.CategoryLocales.FirstOrDefault(c => c.LanguageId == languageId).Name,
            //        FuelType = c.Fueltype.FuelTypeLocales.FirstOrDefault(c => c.LanguageId == languageId).Name,
            //        GearType = c.GearType.GearTypeLocales.FirstOrDefault(c => c.LanguageId == languageId).Name,
            //        Currency = c.Currency.Name,
            //        ReleaseYear = c.Year.Value,
            //        OwnerName = c.Owner.Name,
            //        HorsePower = c.HorsePower,
            //        SeatCount = c.SeatCount,
            //        OwnerNumber = c.Owner.Number,
            //        MileageType = c.MileageType.Name,
            //        Description = c.Description,
            //        EngineVolume = c.EngineVolume,
            //        CreditAvailable = c.CreditAvailable,
            //        BarterAvailable = c.BarterAvailable,
            //        Images = c.Images.Select(i => i.Url).ToList(),
            //        Features = c.Features.Select(f => f.FeatureLocales.FirstOrDefault(c => c.LanguageId == languageId).Name).ToList(),
            //        AutoSalonTitle = c.AutoSalon.Title,
            //        AutoSalonNumber = c.AutoSalon.Number,
            //        AutoSalonLogoUrl = c.AutoSalon.LogoUrl,
            //        AutoSalonCoverUrl = c.AutoSalon.CoverUrl,
            //        AutoSalonLocationUrl = c.AutoSalon.LocationUrl,
            //        AutoSalonSlogan = c.AutoSalon.AutoSalonLocales.FirstOrDefault(c => c.LanguageId == languageId).Slogan,
            //        AutoSalonDescription = c.AutoSalon.AutoSalonLocales.FirstOrDefault(c => c.LanguageId == languageId).Description,
            //        AutoSalonLocation = c.AutoSalon.AutoSalonLocales.FirstOrDefault(c => c.LanguageId == languageId).Location
            //    });
            #endregion

            // Way 2 with Project To
            CarDetailsDTO carDetailsDto =
                  await _carDal.SelectSingleWithProjection<CarDetailsDTO>(mapper: _mapper,
                                                                          languageId: languageId,
                                                                          filter: c => c.Id == carId);

            List<CarDTO> relatedCarDtos =
                  await _carDal.GetListWithProjection<CarDTO>(mapper: _mapper,
                                                              pageNumber: 1,
                                                              pageSize: 20,
                                                              languageId: languageId,
                                                              filter: c => c.CategoryId == carDetailsDto.CategoryId && c.Id != carId,
                                                              orderBy: null);

            carDetailsDto.RelatedCars = relatedCarDtos;
            return carDetailsDto;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<CarDTO>> GetCarsWithPagination(GetCarsRequestDTO getCarsRequestDto)
    {
        Func<IQueryable<Car>, IOrderedQueryable<Car>>? orderBy = null;

        if (!string.IsNullOrEmpty(getCarsRequestDto.Sort))
        {
            orderBy = getCarsRequestDto.Sort switch
            {
                "price_low_to_high" => q => q.OrderBy(c => c.Price),
                "price_high_to_low" => q => q.OrderByDescending(c => c.Price),
                "date" => q => q.OrderBy(c => c.LastUpdated),
                _ => q => q.OrderBy(c => c.Id),
            };
        }

        try
        {
            #region Way1
            // Way 1: Which creates big sql
            //List<Car> cars =
            //    await _carDal.GetListWithPagination(getCarsRequestDto.PageNumber,
            //                                        getCarsRequestDto.PageSize,
            //                                        filter: null,
            //                                        orderBy,
            //                                        include: source => source.Include(c => c.Brand)
            //                                                                 .Include(c => c.Year)
            //                                                                 .Include(c => c.Model)
            //                                                                 .Include(c => c.Images)
            //                                                                 .Include(c => c.Currency)
            //                                                                 .Include(c => c.MileageType)
            //                                                                 .Include(c => c.Region)
            //                                                                 .ThenInclude(r => r.RegionLocales));


            //List<CarDTO> carDtos = _mapper.Map<List<Car>, List<CarDTO>>(cars);
            #endregion

            // Way 2: Instead of writing select by hand, ProjectTo does it automatically and
            // gets only values which are in need
            List<CarDTO> carDtos = await _carDal.GetListWithProjection<CarDTO>(mapper: _mapper,
                                                                               pageNumber: getCarsRequestDto.PageNumber,
                                                                               pageSize: getCarsRequestDto.PageSize,
                                                                               languageId: getCarsRequestDto.LanguageId,
                                                                               filter: null,
                                                                               orderBy: orderBy);
            return carDtos;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
