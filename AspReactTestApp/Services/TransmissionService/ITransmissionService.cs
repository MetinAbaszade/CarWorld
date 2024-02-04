using AspReactTestApp.DTO;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.TransmissionService;

public interface ITransmissionService
{
    public Task<ResponseDTO> AddTransmission(Transmission transmission);
    public Task<ResponseDTO> RemoveTransmissionById(int Id);
    public Task<List<GenericEntityDTO>> GetAllTransmissions(int languageId);
}
