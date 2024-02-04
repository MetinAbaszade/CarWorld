using AspReactTestApp.DTO;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.FueltypeService;

public interface IFuelTypeService
{
    public Task<ResponseDTO> AddFuelType(FuelType fuelType);
    public Task<ResponseDTO> RemoveFuelTypeById(int Id);
    public Task<List<GenericEntityDTO>> GetAllFuelTypes(int languageId);
}
