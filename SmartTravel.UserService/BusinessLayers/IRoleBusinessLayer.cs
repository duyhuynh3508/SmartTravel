using SmartTravel.Shared.ResponseExtension;
using SmartTravel.Shared.Entities;
using SmartTravel.UserService.Helper.ModelMapping.Role;
using SmartTravel.UserService.Helper.ModelMapping.User;
using SmartTravel.Shared.Models.Role;
using SmartTravel.Shared.Models.User;
using SmartTravel.UserService.Repositories;

namespace SmartTravel.UserService.Services
{
    public interface IRoleBusinessLayer
    {
        Task<Response> CreateRole(CreateRoleModel request);
        Task<Response> UpdateRole(UpdateRoleModel request);
        Task<Response> DeleteRole(int id);
        Task<Response> GetRoleById(int id);
        Task<Response> GetAllRoles();
    }

    public class RoleBusinessLayer : IRoleBusinessLayer
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleMapping _roleMapping;
        public RoleBusinessLayer(IRoleRepository roleRepository, IRoleMapping roleMapping)
        {
            _roleRepository = roleRepository;
            _roleMapping = roleMapping;
        }

        public async Task<Response> CreateRole(CreateRoleModel request)
        {
            if (request == null || string.IsNullOrEmpty(request.RoleName))
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Can not create the empty role"));

            var role = await _roleRepository.GetByNameAsync(request.RoleName);

            if (role != null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "The role already existed"));

            var entity = (RoleEntity)_roleMapping.ToEntity(request);

            var response = await _roleRepository.CreateAsync(entity);

            return await Task.FromResult(response);
        }

        public async Task<Response> DeleteRole(int id)
        {
            var response = await _roleRepository.DeleteAsync(id);

            return await Task.FromResult(response);
        }

        public async Task<Response> GetAllRoles()
        {
            var listRoles = (List<RoleModel>)_roleMapping.ToListModels(await _roleRepository.GetAllAsync());

            if (listRoles == null || listRoles.Count == 0)
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "No roles found"));
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", null, listRoles));
        }

        public async Task<Response> GetRoleById(int id)
        {
            var role = (RoleModel)_roleMapping.ToModel(await _roleRepository.GetByIdAsync(id));

            if (role == null)
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Error, $"Cannot find role by id: {id}"));
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", role));
        }

        public async Task<Response> UpdateRole(UpdateRoleModel request)
        {
            if (request == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Invalid role"));

            var userEnity = await _roleRepository.GetByIdAsync((int)request.RoleId);

            if (userEnity == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Cannot find the role"));

            var roleToUpdate = new RoleEntity { RoleId = (int)request.RoleId, RoleName = request.RoleName };

            var response = _roleRepository.UpdateAsync(roleToUpdate);

            return await response;
        }
    }
}
