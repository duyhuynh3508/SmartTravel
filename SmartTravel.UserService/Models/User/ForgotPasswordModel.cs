namespace SmartTravel.Shared.Models.User
{
    public class ForgotPasswordModel : BaseModel
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
