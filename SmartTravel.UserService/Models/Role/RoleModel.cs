using System.ComponentModel.DataAnnotations;
using SmartTravel.Shared.Extension.Enumerators;

namespace SmartTravel.Shared.Models.Role
{
    public class RoleModel : BaseModel
    {
        public RoleEnum RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
