using AspReactTestApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.Services.CarService;
using AspReactTestApp.Entities.Concrete.CarRelated;

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

        [HttpGet("getcardetails")]
        public async Task<ActionResult<Car>> GetCarDetails(int id)
        {
            var car = await _carService.GetCarDetails(id);
            return car;

        }

        [HttpGet("getcardetails")]
        public async Task<ActionResult<List<Car>>> GetCars(int pageNumber, int pageSize = 20, string sort = "")
        {
            var carList = await _carService.GetCarsWithPagination(pageNumber, pageSize, sort);
            return carList;
        }
    }
}
