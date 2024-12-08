using Microsoft.AspNetCore.Mvc;
using SmartTravel.Shared.ResponseExtension;
using SmartTravel.BookingService.Services;
using SmartTravel.BookingService.Models.HotelDetail;

namespace SmartTravel.BookingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelDetailController : ControllerBase
    {
        private readonly IHotelDetailBusinessLayer _hotelDetailService;

        public HotelDetailController(IHotelDetailBusinessLayer hotelDetailService)
        {
            _hotelDetailService = hotelDetailService;
        }

        [HttpPost]
        [Route("createHotelDetail")]
        public async Task<IActionResult> CreateHotelDetail([FromBody] HotelDetailCreateModel request)
        {
            var response = await _hotelDetailService.CreateAsync(request);

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
        [Route("updateHotelDetail")]
        public async Task<IActionResult> UpdateHotelDetail([FromBody] HotelDetailUpdateModel request)
        {
            var response = await _hotelDetailService.UpdateAsync(request);

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
        [Route("deleteHotelDetail/{id}")]
        public async Task<IActionResult> DeleteHotelDetail(int id)
        {
            var response = await _hotelDetailService.DeleteAsync(id);

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
        [Route("getHotelDetailById/{id}")]
        public async Task<IActionResult> GetHotelDetailById(int id)
        {
            var response = await _hotelDetailService.GetByIdAsync(id);

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
        [Route("getAllHotelDetails")]
        public async Task<IActionResult> GetAllHotelDetails()
        {
            var response = await _hotelDetailService.GetAllAsync();

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
