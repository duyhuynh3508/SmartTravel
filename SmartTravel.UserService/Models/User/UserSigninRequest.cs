namespace SmartTravel.UserService.Models.User
{
    public class UserSigninRequest
    {
        public required string UserNameOrEmail { get; set; }
        public required string Password { get; set; }
    }
}
