using AutoMapper;
using AspReactTestApp.DTO;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Mappings;

public class CarToCarDtoMappingProfile : Profile
{
    public CarToCarDtoMappingProfile()
    {
        int? languageId = null;
        CreateMap<Car, CarDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region.RegionLocales.FirstOrDefault(r => r.LanguageId == languageId).Name))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.EngineVolume, opt => opt.MapFrom(src => src.EngineVolume))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.Name))
            .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.Year.Value))
            .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Mileage))
            .ForMember(dest => dest.MileageType, opt => opt.MapFrom(src => src.MileageType.Name))
            .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault().Url));
    }
}
