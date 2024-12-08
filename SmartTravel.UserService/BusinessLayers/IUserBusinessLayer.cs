using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using Polly;
using SmartTravel.Shared.ResponseExtension;
using SmartTravel.Shared.Entities;
using SmartTravel.UserService.Helper.ModelMapping.User;
using SmartTravel.Shared.Models.User;
using SmartTravel.UserService.Repositories;

namespace SmartTravel.UserService.Services
{
    public interface IUserBusinessLayer
    {
        Task<Response> Signin(UserSigninModel user);
        Task<Response> CreateUser(CreateUserModel user);
        Task<Response> UpdateUser(UpdateUserModel user);
        Task<Response> DeleteUser(int id);
        Task<Response> GetUserById(int id);
        Task<Response> GetUsersByIds(IEnumerable<int> ids);
        Task<Response> GetAllUsers();
        Task<Response> ForgotPasswordAsync(ForgotPasswordModel request);
    }

    public class UserBusinessLayer : IUserBusinessLayer
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserMapping _userMapping;
        private readonly IRoleRepository _roleRepository;

        public UserBusinessLayer(IUserRepository repository, IUserMapping userMapping, IRoleRepository roleRepository)
        {
            _userRepository = repository;
            _userMapping = userMapping;
            _roleRepository = roleRepository;
        }

        public async Task<Response> CreateUser(CreateUserModel user)
        {
            if (user == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Can not create the empty user"));

            bool isEmailExisted = await _userRepository.CheckEmailExistedAsync(user.Email);

            if (isEmailExisted)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "The email already existed"));

            bool isUserNameExisted = await _userRepository.CheckUserNameExistedAsync(user.UserName);
            if (isUserNameExisted)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "The username already existed"));

            var entity = (UserEntity)_userMapping.ToEntity(user);

            var response = await _userRepository.CreateAsync(entity);

            return await Task.FromResult(response);
        }

        public async Task<Response> DeleteUser(int id)
        {
            var response = await _userRepository.DeleteAsync(id);

            return await Task.FromResult(response);
        }

        public async Task<Response> GetAllUsers()
        {
            var listUsers = (List<UserModel>)_userMapping.ToListModels(await _userRepository.GetAllAsync());

            if (listUsers == null || listUsers.Count == 0)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "No users found"));

            var listRoles = await _roleRepository.GetByIdsAsync(listUsers.Select(x => (int)x.RoleId).Distinct());

            if (listRoles != null && listRoles.Any())
            {
                listUsers = (List<UserModel>)_userMapping.ToListReferedModels(listUsers, listRoles);
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", null, listUsers));
        }

        public async Task<Response> GetUserById(int id)
        {
            var user = (UserModel)_userMapping.ToModel(await _userRepository.GetByIdAsync(id));

            if (user == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, $"Can not find specific user by id: {id}"));

            var role = await _roleRepository.GetByIdAsync((int)user.RoleId);

            if (role != null)
            {
                user = (UserModel)_userMapping.ToReferedModel(user, role);
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", user));
        }

        public async Task<Response> Signin(UserSigninModel user)
        {
            if (user == null)
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Invalid user"));
            }

            bool isSuccess = await _userRepository.CheckUserLoginAsync(user.UserNameOrEmail, user.Password);

            if (isSuccess)
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Success, "Login successfully"));
            }
            else
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Incorrect username/email or password"));
            }
        }

        public async Task<Response> ForgotPasswordAsync(ForgotPasswordModel request)
        {
            if (request == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Cannot find the account!"));

            if (string.IsNullOrEmpty(request.Email))
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Invalid email"));

            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Cannot find the email"));

            if (!await _userRepository.CheckUserLoginAsync(request.Email, request.OldPassword))
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "The old password is incorrect"));

            if (request.NewPassword !=  request.ConfirmPassword)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "The new password and confirm password are not match"));

            var userEntity = user;
            userEntity.Password = request.NewPassword;

            var response = await _userRepository.UpdateAsync(userEntity);

            return response;
        }

        public async Task<Response> UpdateUser(UpdateUserModel userModel)
        {
            if (userModel == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Invalid user"));

            var userEnity = await _userRepository.GetByIdAsync(userModel.UserId);

            if (userEnity == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Cannot find the user"));

            var userToUpdate = (UserEntity)_userMapping.ToEntity((BaseEntity)userEnity, userModel);

            var response = await _userRepository.UpdateAsync(userToUpdate);

            return response;
        }

        public async Task<Response> GetUsersByIds(IEnumerable<int> ids)
        {
            var listUsers = (List<UserModel>)_userMapping.ToListModels(await _userRepository.GetByIdsAsync(ids));

            if (listUsers == null || listUsers.Count == 0)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "No users found"));

            var listRoles = await _roleRepository.GetByIdsAsync(listUsers.Select(x => (int)x.RoleId).Distinct());

            if (listRoles != null && listRoles.Any())
            {
                listUsers = (List<UserModel>)_userMapping.ToListReferedModels(listUsers, listRoles);
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", null, listUsers));
        }
    }
}
