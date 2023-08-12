using AspReactTestApp.Dto;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.FueltypeService
{
    public interface IFuelTypeService
    {
        public Task<ResponseDto> AddFuelType(FuelType fuelType);
        public Task<ResponseDto> RemoveFuelTypeById(int Id);
        public Task<List<GenericEntityDto>> GetAllFuelTypes(int languageId);
    }
}
