using Microsoft.AspNetCore.Mvc;
using SmartTravel.Shared.ResponseExtension;
using SmartTravel.BookingService.Services;
using SmartTravel.BookingService.Models.FlightDetail;

namespace SmartTravel.BookingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightDetailController : ControllerBase
    {
        private readonly IFlightDetailBusinessLayer _flightDetailService;

        public FlightDetailController(IFlightDetailBusinessLayer flightDetailService)
        {
            _flightDetailService = flightDetailService;
        }

        [HttpPost]
        [Route("createFlightDetail")]
        public async Task<IActionResult> CreateFlightDetail([FromBody] FlightDetailCreateModel request)
        {
            var response = await _flightDetailService.CreateAsync(request);

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
        [Route("updateFlightDetail")]
        public async Task<IActionResult> UpdateFlightDetail([FromBody] FlightDetailUpdateModel request)
        {
            var response = await _flightDetailService.UpdateAsync(request);

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
        [Route("deleteFlightDetail/{id}")]
        public async Task<IActionResult> DeleteFlightDetail(int id)
        {
            var response = await _flightDetailService.DeleteAsync(id);

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
        [Route("getFlightDetailById/{id}")]
        public async Task<IActionResult> GetFlightDetailById(int id)
        {
            var response = await _flightDetailService.GetByIdAsync(id);

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
        [Route("getAllFlightDetails")]
        public async Task<IActionResult> GetAllFlightDetails()
        {
            var response = await _flightDetailService.GetAllAsync();

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
