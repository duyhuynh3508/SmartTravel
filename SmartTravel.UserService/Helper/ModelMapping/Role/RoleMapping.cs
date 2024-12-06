using SmartTravel.UserService.Entities;
using SmartTravel.UserService.Models.Role;

namespace SmartTravel.UserService.Helper.ModelMapping.Role
{
    public static class RoleMapping
    {
        public static RoleEntity ToEntity(RoleModel model)
        {
            RoleEntity role = new RoleEntity()
            {
                RoleId = model.RoleId,
                RoleName = model.RoleName,
            };
            return role;
        }
    }
}
