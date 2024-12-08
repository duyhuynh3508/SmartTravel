using Microsoft.EntityFrameworkCore;
using SmartTravel.Shared.Interface;
using SmartTravel.Shared.Logging;
using SmartTravel.Shared.ResponseExtension;
using SmartTravel.UserService.DatabaseContext;
using SmartTravel.Shared.Entities;
using System.Linq.Expressions;

namespace SmartTravel.UserService.Repositories
{
    public interface IUserRepository : ICommonInterface<UserEntity>
    {
        Task<bool> CheckUserNameExistedAsync(string userName);
        Task<bool> CheckEmailExistedAsync(string email);
        Task<bool> CheckUserLoginAsync(string emailOrUserName, string password);
        Task<UserEntity> GetUserByEmailAsync(string email);
        Task<UserEntity> GetUserByUserNameAsync(string userName);
        Task<IEnumerable<UserEntity>> GetUsersByRoleAsync(int roleId);

    }
    public class UserRepository : IUserRepository
    {
        private readonly UserServiceDbContext _context;

        public UserRepository(UserServiceDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckUserNameExistedAsync(string userName)
        {
            bool isExist =  await _context.Users.AnyAsync(u => u.UserName == userName);

            return isExist;
        }
        public async Task<bool> CheckEmailExistedAsync(string email)
        {
            bool isExist = await _context.Users.AnyAsync(u => u.Email == email);

            return isExist;
        }

        public async Task<bool> CheckUserLoginAsync(string emailOrUserName, string password)
        {
            bool isExist = await _context.Users.AnyAsync(u => (u.Email == emailOrUserName || u.UserName == emailOrUserName) && u.Password == password);

            return isExist;
        }

        public async Task<Response> CreateAsync(UserEntity entity)
        {
            if (entity == null)
                return new Response(ResponseResultEnum.Error, "Entity cannot be null");
            try
            {
                await _context.Users.AddAsync(entity);
                await _context.SaveChangesAsync();
                return new Response(ResponseResultEnum.Error, "Create new user successfully", entity);
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error creating user: {ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(int id)
        {
            try
            {
                var user = await GetByIdAsync(id);

                if(user == null)
                    return new Response(ResponseResultEnum.Error, "Cannot find user");

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return new Response(ResponseResultEnum.Success, "User deleted successfully", user);

            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error deleting user: {ex.Message}");
            }
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UserEntity> GetByAsync(Expression<Func<UserEntity, bool>> predicate)
        {
            return await _context.Users.FirstOrDefaultAsync(predicate);
        }

        public async Task<UserEntity> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }


        public async Task<UserEntity> GetByNameAsync(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.FirstName + ' ' + u.LastName == name);
        }

        public async Task<Response> UpdateAsync(UserEntity entity)
        {
            if (entity == null)
                return new Response(ResponseResultEnum.Error, "Entity cannot be null");

            try
            {
                _context.Users.Update(entity);
                await _context.SaveChangesAsync();

                return new Response(ResponseResultEnum.Success, "User updated successfully", entity);
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error updating user: {ex.Message}");
            }
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            UserEntity userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            return userEntity;
        }

        public async Task<UserEntity> GetUserByUserNameAsync(string userName)
        {
            UserEntity userEntity = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);

            return userEntity;
        }

        public async Task<IEnumerable<UserEntity>> GetUsersByRoleAsync(int roleId)
        {
            var users = _context.Users.Where(u => u.RoleId == roleId);

            return users;
        }

        public async Task<IEnumerable<UserEntity>> GetByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<UserEntity>();
            }

            return await _context.Users
                                 .Where(user => ids.Contains(user.UserId))
                                 .ToListAsync();
        }
    }
}
