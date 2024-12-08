using SmartTravel.Shared.Extension.Enumerators;

namespace SmartTravel.Shared.Models.Role
{
    public class CreateRoleModel : BaseModel
    {
        public RoleEnum RoleId { get; set; }
        public required string RoleName { get; set; }
    }
}
