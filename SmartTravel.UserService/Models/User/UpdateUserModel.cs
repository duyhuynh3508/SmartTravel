using SmartTravel.Shared.Extension.Enumerators;

namespace SmartTravel.Shared.Models.User
{
    public class UpdateUserModel : BaseModel
    {
        public int UserId { get; set; }
        public RoleEnum RoleId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
