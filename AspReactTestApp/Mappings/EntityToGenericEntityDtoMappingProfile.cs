using AutoMapper;
using AspReactTestApp.DTO;
using AspReactTestApp.Entities.Concrete.CarRelated;
using TurboazFetching.Entities;

namespace AspReactTestApp.Mappings;

public class EntityToGenericEntityDTOMappingProfile : Profile
{
    public EntityToGenericEntityDTOMappingProfile()
    {
        CreateMap<Model, GenericEntityDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<Category, GenericEntityDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryLocales.FirstOrDefault().Name));

        CreateMap<Color, GenericEntityDTO>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ColorLocales.FirstOrDefault().Name));

        CreateMap<Feature, GenericEntityDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FeatureLocales.FirstOrDefault().Name));

        CreateMap<FuelType, GenericEntityDTO>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FuelTypeLocales.FirstOrDefault().Name));

        CreateMap<GearType, GenericEntityDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GearTypeLocales.FirstOrDefault().Name));

        CreateMap<Market, GenericEntityDTO>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MarketLocales.FirstOrDefault().Name));

        CreateMap<Region, GenericEntityDTO>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RegionLocales.FirstOrDefault().Name));

        CreateMap<Transmission, GenericEntityDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TransmissionLocales.FirstOrDefault().Name));

    }
}
