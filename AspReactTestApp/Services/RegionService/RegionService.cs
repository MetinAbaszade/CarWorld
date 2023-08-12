using AspReactTestApp.DTOs;
using Microsoft.EntityFrameworkCore;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Entities.Concrete.CarRelated;
using AspReactTestApp.Dto;
using AutoMapper;

namespace AspReactTestApp.Services.RegionService
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionService(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> AddRegion(Region region)
        {
            ResponseDto responseDto = new();
            try
            {
                await _regionRepository.Add(region);
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Region Added Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<ResponseDto> RemoveRegionById(int id)
        {
            ResponseDto responseDto = new();
            try
            {
                var region = await _regionRepository.Get(r => r.Id == id);
                if (region != null)
                {
                    await _regionRepository.Delete(region);
                    responseDto.Message = "Region Not Found!";
                    return responseDto;
                }
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Region Removed Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<List<GenericEntityDto>> GetAllRegions(int languageId)
        {
            try
            {
                var regionDtos = await _regionRepository.Select(
                    select: r => new GenericEntityDto
                    {
                        Id = r.Id,
                        Name = r.RegionLocales.SingleOrDefault(rl => rl.LanguageId == languageId).Name
                    });

                return regionDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
