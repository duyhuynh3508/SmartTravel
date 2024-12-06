using System.ComponentModel.DataAnnotations;

namespace SmartTravel.UserService.Models.User
{
    public record UserModel(
            int UserId,
            [Required, Range(1, int.MaxValue)] int RoleId,
            [Required] string UserName,
            [Required] string Email,
            [Required] string Password,
            [Required] string PasswordHash,
            [Required] string FirstName,
            [Required] string LastName,
            [Required] DateTime DateOfBirth,
            [Required] DateTime CreatedOn,
            DateTime LastUpdatedOn
        );
}
