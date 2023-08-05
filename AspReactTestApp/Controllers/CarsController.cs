using AspReactTestApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.Services.CarService;
using AspReactTestApp.Entities.Concrete.CarRelated;
using AspReactTestApp.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspReactTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpPost("addcar")]
        public async Task<ActionResult<ResponseDto>> AddCar(Car car)
        {
            var result = await _carService.AddCar(car);
            return Ok(result);
        }

        [HttpDelete("deletecar")]
        public async Task<ActionResult<ResponseDto>> DeleteCar(int id)
        {
            var result = await _carService.RemoveCar(id);
            return Ok(result);
        }

        [HttpPost("getcardetails/{id}")]
        public async Task<ActionResult<CarDetailsDto>> GetCarDetails(int id)
        {
            var carDetailsDto = await _carService.GetCarDetails(id);
            return carDetailsDto;
        }

        [HttpPost("getcars")]
        public async Task<ActionResult<List<CarDto>>> GetCars(GetCarsRequestDto getCarsRequestDto)
        {
            var carDtoList = await _carService.GetCarsWithPagination(getCarsRequestDto);
            return carDtoList;
        }
    }
}
