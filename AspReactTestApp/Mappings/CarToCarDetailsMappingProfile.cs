using AspReactTestApp.Dto;
using AspReactTestApp.Entities.Concrete.CarRelated;
using AutoMapper;

namespace AspReactTestApp.Mappings
{
    public class CarToCarDetailsMappingProfile : Profile
    {
        public CarToCarDetailsMappingProfile()
        {
            CreateMap<Car, CarDetailsDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Mileage))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model.Name))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color.ColorLocales.FirstOrDefault().Name))
                .ForMember(dest => dest.Market, opt => opt.MapFrom(src => src.Market.MarketLocales.FirstOrDefault().Name))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region.RegionLocales.FirstOrDefault().Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.CategoryLocales.FirstOrDefault().Name))
                .ForMember(dest => dest.FuelType, opt => opt.MapFrom(src => src.Fueltype.FuelTypeLocales.FirstOrDefault().Name))
                .ForMember(dest => dest.GearType, opt => opt.MapFrom(src => src.GearType.GearTypeLocales.FirstOrDefault().Name))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.Name))
                .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.Year.Value))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.Name))
                .ForMember(dest => dest.HorsePower, opt => opt.MapFrom(src => src.HorsePower))
                .ForMember(dest => dest.SeatCount, opt => opt.MapFrom(src => src.SeatCount))
                .ForMember(dest => dest.OwnerNumber, opt => opt.MapFrom(src => src.Owner.Number))
                .ForMember(dest => dest.MileageType, opt => opt.MapFrom(src => src.MileageType.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.EngineVolume, opt => opt.MapFrom(src => src.EngineVolume))
                .ForMember(dest => dest.CreditAvailable, opt => opt.MapFrom(src => src.CreditAvailable))
                .ForMember(dest => dest.BarterAvailable, opt => opt.MapFrom(src => src.BarterAvailable))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.Url).ToList()))
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.Features.Select(f => f.FeatureLocales.FirstOrDefault().Name).ToList()))
                .ForMember(dest => dest.AutoSalonTitle, opt => opt.MapFrom(src => src.AutoSalon.Title))
                .ForMember(dest => dest.AutoSalonNumber, opt => opt.MapFrom(src => src.AutoSalon.Number))
                .ForMember(dest => dest.AutoSalonLogoUrl, opt => opt.MapFrom(src => src.AutoSalon.LogoUrl))
                .ForMember(dest => dest.AutoSalonCoverUrl, opt => opt.MapFrom(src => src.AutoSalon.CoverUrl))
                .ForMember(dest => dest.AutoSalonLocationUrl, opt => opt.MapFrom(src => src.AutoSalon.LocationUrl))
                .ForMember(dest => dest.AutoSalonSlogan, opt => opt.MapFrom(src => src.AutoSalon.AutoSalonLocales.FirstOrDefault().Slogan))
                .ForMember(dest => dest.AutoSalonDescription, opt => opt.MapFrom(src => src.AutoSalon.AutoSalonLocales.FirstOrDefault().Description))
                .ForMember(dest => dest.AutoSalonLocation, opt => opt.MapFrom(src => src.AutoSalon.AutoSalonLocales.FirstOrDefault().Location));
        }
    }
}

