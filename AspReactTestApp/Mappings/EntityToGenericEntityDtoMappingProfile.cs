using AspReactTestApp.Dto;
using AspReactTestApp.Entities.Concrete.CarRelated;
using AutoMapper;
using TurboazFetching.Entities;

namespace AspReactTestApp.Mappings
{
    public class EntityToGenericEntityDtoMappingProfile : Profile
    {
        public EntityToGenericEntityDtoMappingProfile()
        {
            CreateMap<Model, GenericEntityDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Category, GenericEntityDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryLocales.FirstOrDefault().Name));

            CreateMap<Color, GenericEntityDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ColorLocales.FirstOrDefault().Name));

            CreateMap<Feature, GenericEntityDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FeatureLocales.FirstOrDefault().Name));

            CreateMap<FuelType, GenericEntityDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FuelTypeLocales.FirstOrDefault().Name));

            CreateMap<GearType, GenericEntityDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GearTypeLocales.FirstOrDefault().Name));

            CreateMap<Market, GenericEntityDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MarketLocales.FirstOrDefault().Name));

            CreateMap<Region, GenericEntityDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RegionLocales.FirstOrDefault().Name));

            CreateMap<Transmission, GenericEntityDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TransmissionLocales.FirstOrDefault().Name));

        }
    }
}
