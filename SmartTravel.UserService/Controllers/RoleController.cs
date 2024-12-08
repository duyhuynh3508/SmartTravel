using Microsoft.AspNetCore.Mvc;
using SmartTravel.Shared.ResponseExtension;
using SmartTravel.Shared.Models.Role;
using SmartTravel.UserService.Services;

namespace SmartTravel.UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleBusinessLayer _roleService;

        public RoleController(IRoleBusinessLayer roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        [Route("createRole")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleModel request)
        {
            var response = await _roleService.CreateRole(request);

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
        [Route("updateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleModel request)
        {
            var response = await _roleService.UpdateRole(request);

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
        [Route("deleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var response = await _roleService.DeleteRole(id);

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
        [Route("getRoleById/{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var response = await _roleService.GetRoleById(id);

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
        [Route("getAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _roleService.GetAllRoles();

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
