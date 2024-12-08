using Microsoft.EntityFrameworkCore;
using SmartTravel.Shared.Logging;
using SmartTravel.Shared.ResponseExtension;
using SmartTravel.UserService.DatabaseContext;
using SmartTravel.Shared.Entities;

namespace SmartTravel.UserService.Repositories
{
    public interface IPermissionRepository
    {
        Task<PermissionEntity> GetPermissionByRoleIdAsync(int roleId);
        Task<IEnumerable<PermissionEntity>> GetAllPermissionsAsync();
        Task<Response> CreatePermissionAsync(PermissionEntity entity);
        Task<Response> UpdatePermissionAsync(PermissionEntity entity);
    }

    public class PermissionRepository : IPermissionRepository
    {
        private readonly UserServiceDbContext _context;

        public PermissionRepository(UserServiceDbContext context)
        {
            _context = context;
        }

        public async Task<Response> CreatePermissionAsync(PermissionEntity entity)
        {
            if (entity == null)
                return new Response(ResponseResultEnum.Error, "Entity cannot be null");
            try
            {
                await _context.Permissions.AddAsync(entity);
                await _context.SaveChangesAsync();
                return new Response(ResponseResultEnum.Success, "Create new role permission successfully");
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error creating role permission: {ex.Message}");
            }
        }

        public async Task<IEnumerable<PermissionEntity>> GetAllPermissionsAsync()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<PermissionEntity> GetPermissionByRoleIdAsync(int roleId)
        {
            return await _context.Permissions.FirstOrDefaultAsync(p => p.RoleId == roleId);
        }

        public async Task<Response> UpdatePermissionAsync(PermissionEntity entity)
        {
            if (entity == null)
                return new Response(ResponseResultEnum.Error, "Entity cannot be null");

            try
            {
                _context.Permissions.Update(entity);
                await _context.SaveChangesAsync();

                return new Response(ResponseResultEnum.Success, "Role permission updated successfully", entity);
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error updating role permission: {ex.Message}");
            }
        }
    }
}
