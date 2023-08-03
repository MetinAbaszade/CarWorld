using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Dto;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AspReactTestApp.Services.TransmissionService
{
    public class TransmissionService : ITransmissionService
    {
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly IMapper _mapper;

        public TransmissionService(ITransmissionRepository transmissionRepository, IMapper mapper)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> AddTransmission(Transmission transmission)
        {
            ResponseDto responseDto = new();
            try
            {
                await _transmissionRepository.Add(transmission);
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Transmission Added Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<ResponseDto> RemoveTransmissionById(int id)
        {
            ResponseDto responseDto = new();
            try
            {
                var transmission = await _transmissionRepository.Get(t => t.Id == id);
                if (transmission != null)
                {
                    await _transmissionRepository.Delete(transmission);
                    responseDto.Message = "Transmission Not Found!";
                    return responseDto;
                }
                responseDto.IsSuccessfull = true;
                responseDto.Message = "Transmission Removed Successfully!";
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        public async Task<List<GenericEntityDto>> GetAllTransmissions(string language)
        {
            try
            {
                var transmissions = await _transmissionRepository.GetList(
                    filter: t => t.TransmissionLocales.Any(tl => tl.Language.DisplayName == language),
                    orderBy: null,
                    include: source => source.Include(t => t.TransmissionLocales));

                List<GenericEntityDto> transmissionDtos = _mapper.Map<List<GenericEntityDto>>(transmissions);
                return transmissionDtos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
