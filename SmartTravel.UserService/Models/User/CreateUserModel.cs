using SmartTravel.Shared.Extension.Enumerators;

namespace SmartTravel.Shared.Models.User
{
    public class CreateUserModel : BaseModel
    {
        public RoleEnum RoleId { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
