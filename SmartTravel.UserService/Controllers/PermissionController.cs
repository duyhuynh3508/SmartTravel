using Microsoft.AspNetCore.Mvc;
using SmartTravel.Shared.ResponseExtension;
using SmartTravel.Shared.Models.Permission;
using SmartTravel.UserService.Services;

namespace SmartTravel.UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionBusinessLayer _permissionService;

        public PermissionController(IPermissionBusinessLayer permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        [Route("getPermissionByRoleId/{roleId}")]
        public async Task<IActionResult> GetPermissionByRoleId(int roleId)
        {
            var response = await _permissionService.GetPermissionByRoleIdAsync(roleId);

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
        [Route("getAllPermissions")]
        public async Task<IActionResult> GetAllPermissions()
        {
            var response = await _permissionService.GetAllPermissionsAsync();

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
        [Route("createPermission")]
        public async Task<IActionResult> CreatePermission([FromBody] PermissionCreateModel request)
        {
            var response = await _permissionService.CreatePermissionAsync(request);

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
        [Route("updatePermission")]
        public async Task<IActionResult> UpdatePermission([FromBody] PermissionUpdateModel request)
        {
            var response = await _permissionService.UpdatePermissionAsync(request);

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
