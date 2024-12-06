using Polly;
using SmartTravel.Shared.ResponseExtension;
using SmartTravel.UserService.Entities;
using SmartTravel.UserService.Helper.ModelMapping.User;
using SmartTravel.UserService.Models.User;
using SmartTravel.UserService.Repositories;

namespace SmartTravel.UserService.Services.UserService
{
    public interface IUserService
    {
        Task<Response> Signin(UserSigninRequest user);
        Task<Response> CreateUser(CreateUserRequest user);
        Task<Response> UpdateUser(UpdateUserRequest user);
        Task<Response> DeleteUser(int id);
        Task<UserEntity> GetUserById(int id);
        Task<IEnumerable<UserEntity>> GetAllUsers();
        Task<Response> UpdatePasswordAsync(string userNameOrEmail, string oldPassword, string newPassword);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public async Task<Response> CreateUser(CreateUserRequest user)
        {
            if (user == null)
                return await Task.FromResult(new Response(false, "Can not create the empty user"));

            bool isUserExist = await _userRepository.CheckUserExistedAsync(user.Email, user.UserName);

            if(isUserExist)
                return await Task.FromResult(new Response(false, "The user already existed"));

            var entity = UserMapping.ToEntity(user);

            var response = await _userRepository.CreateAsync(entity);

            return await Task.FromResult(response);
        }

        public async Task<Response> DeleteUser(int id)
        {
            var response = await _userRepository.DeleteAsync(id);

            return await Task.FromResult(response);
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsers()
        {
            var response = await _userRepository.GetAllAsync();

            return await Task.FromResult(response);
        }

        public async Task<UserEntity> GetUserById(int id)
        {
            var response = await _userRepository.GetByIdAsync(id);

            return await Task.FromResult(response);
        }

        public async Task<Response> Signin(UserSigninRequest user)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> UpdatePasswordAsync(string userNameOrEmail, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> UpdateUser(UpdateUserRequest user)
        {
            throw new NotImplementedException();
        }
    }
}
