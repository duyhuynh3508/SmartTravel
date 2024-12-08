using Microsoft.AspNetCore.Mvc;
using SmartTravel.Shared.ResponseExtension;
using SmartTravel.Shared.Models.User;
using SmartTravel.UserService.Services;

namespace SmartTravel.UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBusinessLayer _userService;

        public UserController(IUserBusinessLayer userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> Signin([FromBody] UserSigninModel request)
        {
            var response = await _userService.Signin(request);

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
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] CreateUserModel request)
        {
            var response = await _userService.CreateUser(request);

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
        [Route("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel request)
        {
            var response = await _userService.UpdateUser(request);

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
        [Route("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);

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
        [Route("getUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _userService.GetUserById(id);

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
        [Route("getUsersByIds")]
        public async Task<IActionResult> GetUsersByIds([FromQuery] IEnumerable<int> ids)
        {
            var response = await _userService.GetUsersByIds(ids);

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
        [Route("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsers();

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
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel request)
        {
            var response = await _userService.ForgotPasswordAsync(request);

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
