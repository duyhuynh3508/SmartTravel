using SmartTravel.Shared.Interface;
using SmartTravel.Shared.ResponseExtension;
using SmartTravel.UserService.DatabaseContext;
using SmartTravel.UserService.Entities;
using System.Linq.Expressions;

namespace SmartTravel.UserService.Repositories
{
    public interface IUserRepository : ICommonInterface<UserEntity>
    {
        Task<bool> CheckUserExistedAsync(string email, string userName);
    }
    public class UserRepository : IUserRepository
    {
        private readonly UserServiceDbContext _context;

        public UserRepository(UserServiceDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckUserExistedAsync(string email, string userName)
        {
            bool isExist = _context.Users.Any(u => u.Email == email || u.UserName == userName);

            return await Task.FromResult(isExist);
        }

        public async Task<Response> CreateAsync(UserEntity entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new Response(true, "Create new user successfully");
        }

        public async Task<Response> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> GetByAsync(Expression<Func<UserEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> UpdateAsync(UserEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
