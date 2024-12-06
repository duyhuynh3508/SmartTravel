using System.ComponentModel.DataAnnotations;

namespace SmartTravel.UserService.Models.Role
{
    public record RoleModel(
            int RoleId,
            [Required] string RoleName
        );
}
