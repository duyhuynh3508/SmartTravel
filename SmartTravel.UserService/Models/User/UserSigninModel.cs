namespace SmartTravel.Shared.Models.User
{
    public class UserSigninModel : BaseModel
    {
        public required string UserNameOrEmail { get; set; }
        public required string Password { get; set; }
    }
}
