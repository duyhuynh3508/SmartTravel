using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartTravel.Shared.Interface;
using SmartTravel.Shared.Logging;
using SmartTravel.Shared.ResponseExtension;
using SmartTravel.UserService.DatabaseContext;
using SmartTravel.Shared.Entities;

namespace SmartTravel.UserService.Repositories
{
    public interface IRoleRepository : ICommonInterface<RoleEntity>
    {
    }

    public class RoleRepository : IRoleRepository
    {
        private readonly UserServiceDbContext _context;

        public RoleRepository(UserServiceDbContext context)
        {
            _context = context;
        }
        public async Task<Response> CreateAsync(RoleEntity entity)
        {
            if (entity == null)
                return new Response(ResponseResultEnum.Error, "Entity cannot be null");
            try
            {
                await _context.Roles.AddAsync(entity);
                await _context.SaveChangesAsync();
                return new Response(ResponseResultEnum.Success, "Create new role successfully", entity);
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error creating role: {ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(int id)
        {
            try
            {
                var role = await GetByIdAsync(id);

                if (role == null)
                    return new Response(ResponseResultEnum.Error, "Cannot find role");

                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();

                return new Response(ResponseResultEnum.Success, "Role deleted successfully", role);

            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error deleting role: {ex.Message}");
            }
        }

        public async Task<IEnumerable<RoleEntity>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<RoleEntity> GetByAsync(Expression<Func<RoleEntity, bool>> predicate)
        {
            return await _context.Roles.FirstOrDefaultAsync(predicate);
        }

        public async Task<RoleEntity> GetByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public Task<RoleEntity> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RoleEntity>> GetByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<RoleEntity>();
            }

            return await _context.Roles
                                 .Where(e => ids.Contains(e.RoleId))
                                 .ToListAsync();
        }

        public async Task<RoleEntity> GetByNameAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == name);
        }

        public async Task<Response> UpdateAsync(RoleEntity entity)
        {
            if (entity == null)
                return new Response(ResponseResultEnum.Error, "Entity cannot be null");

            try
            {
                _context.Roles.Update(entity);
                await _context.SaveChangesAsync();

                return new Response(ResponseResultEnum.Success, "Role updated successfully");
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error updating user: {ex.Message}");
            }
        }
    }
}
