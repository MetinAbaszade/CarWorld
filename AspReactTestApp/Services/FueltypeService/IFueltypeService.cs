using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Services.FueltypeService
{
    public interface IFueltypeService
    {
        public Task<ResponseDto> AddFueltype(Fueltype fueltype);
        public Task<ResponseDto> RemoveFueltypeById(int Id);
        public Task<List<Fueltype>> GetAllFueltypes(string language = "Az");
    }
}
