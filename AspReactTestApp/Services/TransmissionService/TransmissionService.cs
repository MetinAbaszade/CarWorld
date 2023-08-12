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

        public async Task<List<GenericEntityDto>> GetAllTransmissions(int languageId)
        {
            try
            {
                var transmissionDtos = await _transmissionRepository.Select(
                    select: t => new GenericEntityDto
                    {
                        Id = t.Id,
                        Name = t.TransmissionLocales.SingleOrDefault(tl => tl.LanguageId == languageId).Name
                    });

               return transmissionDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
