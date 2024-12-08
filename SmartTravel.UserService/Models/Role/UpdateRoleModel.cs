using SmartTravel.Shared.Extension.Enumerators;

namespace SmartTravel.Shared.Models.Role
{
    public class UpdateRoleModel
    {
        public RoleEnum RoleId { get; set; }
        public required string RoleName { get; set; }
    }
}
