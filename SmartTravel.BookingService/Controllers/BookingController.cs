using Microsoft.AspNetCore.Mvc;
using SmartTravel.Shared.ResponseExtension;
using SmartTravel.BookingService.Services;
using SmartTravel.BookingService.Models.Booking;

namespace SmartTravel.BookingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingBusinessLayer _bookingService;

        public BookingController(IBookingBusinessLayer bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [Route("createBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateModel request)
        {
            var response = await _bookingService.CreateAsync(request);

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
        [Route("updateBooking")]
        public async Task<IActionResult> UpdateBooking([FromBody] BookingUpdateModel request)
        {
            var response = await _bookingService.UpdateAsync(request);

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
        [Route("deleteBooking/{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var response = await _bookingService.DeleteAsync(id);

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
        [Route("getBookingById/{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var response = await _bookingService.GetByIdAsync(id);

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

