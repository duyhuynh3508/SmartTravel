using SmartTravel.Shared.ResponseExtension;
using SmartTravel.Shared.Entities;
using SmartTravel.UserService.Helper.ModelMapping.Permission;
using SmartTravel.Shared.Models.Permission;
using SmartTravel.UserService.Repositories;

namespace SmartTravel.UserService.Services
{
    public interface IPermissionBusinessLayer
    {
        Task<Response> GetPermissionByRoleIdAsync(int roleId);
        Task<Response> GetAllPermissionsAsync();
        Task<Response> CreatePermissionAsync(PermissionCreateModel model);
        Task<Response> UpdatePermissionAsync(PermissionUpdateModel model);
    }

    public class PermissionBusinessLayer : IPermissionBusinessLayer
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionMapping _permissionMapping;
        private readonly IRoleRepository _roleRepository;
        public PermissionBusinessLayer(IPermissionRepository permissionRepository, IPermissionMapping permissionMapping, IRoleRepository roleRepository) 
        {
            _permissionRepository = permissionRepository;
            _permissionMapping = permissionMapping;
            _roleRepository = roleRepository;
        }

        public async Task<Response> CreatePermissionAsync(PermissionCreateModel model)
        {
            if (model == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Can not create the empty"));

            var isRolePermissionExisted = await _permissionRepository.GetPermissionByRoleIdAsync((int)model.RoleId);

            if (isRolePermissionExisted != null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "The role permission already existed"));

            var permissionToCreate = (SmartTravel.Shared.Entities.PermissionEntity)_permissionMapping.ToEntity(model);

            var response = _permissionRepository.CreatePermissionAsync(permissionToCreate);

            return await response;
        }

        public async Task<Response> GetAllPermissionsAsync()
        {
            var listPermissions = (List<PermissionModel>)_permissionMapping.ToListModels(await _permissionRepository.GetAllPermissionsAsync());

            if (listPermissions == null || listPermissions.Count == 0)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "No permissions found"));

            var listRoles = await _roleRepository.GetByIdsAsync(listPermissions.Select(p => (int)p.RoleId).Distinct());

            if (listRoles != null && listRoles.Any())
            {
                listPermissions = (List<PermissionModel>)_permissionMapping.ToListReferedModels(listPermissions, listRoles);
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", null, listPermissions));
        }

        public async Task<Response> GetPermissionByRoleIdAsync(int roleId)
        {
            var permission = (PermissionModel)_permissionMapping.ToModel(await _permissionRepository.GetPermissionByRoleIdAsync(roleId));

            if (permission == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, $"Cannot find permission by RoleId: {roleId}"));

            var role = await _roleRepository.GetByIdAsync((int)permission.RoleId);

            if (role != null)
            {
                permission = (PermissionModel)_permissionMapping.ToReferedModel(permission, role);
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", permission));
        }

        public async Task<Response> UpdatePermissionAsync(PermissionUpdateModel model)
        {
            if (model == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Invalid role permission"));

            var permissionEntity = await _permissionRepository.GetPermissionByRoleIdAsync((int)model.RoleId);

            if (permissionEntity == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Cannot find the role permission"));

            var permissionToUpdate = (PermissionEntity)_permissionMapping.ToEntity((BaseEntity)permissionEntity, model);

            var response = _permissionRepository.UpdatePermissionAsync(permissionToUpdate);

            return await response;
        }
    }
}
