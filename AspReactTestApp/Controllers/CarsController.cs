using AspReactTestApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.Services.CarService;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CarsController(ICarService carService) : ControllerBase
{
    private readonly ICarService _carService = carService;

    [HttpPost("addcar")]
    public async Task<ActionResult<ResponseDTO>> AddCar(Car car)
    {
        var result = await _carService.AddCar(car);
        return Ok(result);
    }

    [HttpDelete("deletecar")]
    public async Task<ActionResult<ResponseDTO>> DeleteCar(int id)
    {
        var result = await _carService.RemoveCar(id);
        return Ok(result);
    }

    [HttpPost("getcardetails/{carId}/{languageId}")]
    public async Task<ActionResult<CarDetailsDTO>> GetCarDetails(int carId, int languageId)
    {
        var carDetailsDto = await _carService.GetCarDetails(carId, languageId);
        return carDetailsDto;
    }

    [HttpPost("getcars")]
    public async Task<ActionResult<List<CarDTO>>> GetCars(GetCarsRequestDTO getCarsRequestDto)
    {
        var carDtoList = await _carService.GetCarsWithPagination(getCarsRequestDto);
        return carDtoList;
    }
}
