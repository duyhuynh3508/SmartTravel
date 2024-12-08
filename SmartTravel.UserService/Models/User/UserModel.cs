using SmartTravel.Shared.Models;
using SmartTravel.Shared.Extension.Enumerators;
using SmartTravel.Shared.Models.Role;

namespace SmartTravel.Shared.Models.User
{
    public class UserModel : BaseModel
    {
        public int UserId { get; set; }
        public RoleEnum RoleId { get; set; }
        public RoleModel Role { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    } 
}
