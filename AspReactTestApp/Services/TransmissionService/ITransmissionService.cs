using AspReactTestApp.Dto;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.TransmissionService
{
    public interface ITransmissionService
    {
        public Task<ResponseDto> AddTransmission(Transmission transmission);
        public Task<ResponseDto> RemoveTransmissionById(int Id);
        public Task<List<GenericEntityDto>> GetAllTransmissions(string language = "Az");
    }
}
