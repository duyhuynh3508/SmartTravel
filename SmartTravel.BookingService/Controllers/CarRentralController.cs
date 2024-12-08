using Microsoft.AspNetCore.Mvc;
using SmartTravel.Shared.ResponseExtension;
using SmartTravel.BookingService.Services;
using SmartTravel.BookingService.Models.CarRental;

namespace SmartTravel.BookingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarRentalController : ControllerBase
    {
        private readonly ICarRentalBusinessLayer _carRentalService;

        public CarRentalController(ICarRentalBusinessLayer carRentalService)
        {
            _carRentalService = carRentalService;
        }

        [HttpPost]
        [Route("createCarRental")]
        public async Task<IActionResult> CreateCarRental([FromBody] CarRentalCreateModel request)
        {
            var response = await _carRentalService.CreateAsync(request);

            if (response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("updateCarRental")]
        public async Task<IActionResult> UpdateCarRental([FromBody] CarRentalUpdateModel request)
        {
            var response = await _carRentalService.UpdateAsync(request);

            if (response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpDelete]
        [Route("deleteCarRental/{id}")]
        public async Task<IActionResult> DeleteCarRental(int id)
        {
            var response = await _carRentalService.DeleteAsync(id);

            if (response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("getCarRentalById/{id}")]
        public async Task<IActionResult> GetCarRentalById(int id)
        {
            var response = await _carRentalService.GetByIdAsync(id);

            if (response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("getAllCarRentals")]
        public async Task<IActionResult> GetAllCarRentals()
        {
            var response = await _carRentalService.GetAllAsync();

            if (response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
