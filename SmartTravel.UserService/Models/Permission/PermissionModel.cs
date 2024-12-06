using System.ComponentModel.DataAnnotations;

namespace SmartTravel.UserService.Models.Permission
{
    public record PermissionModel(
            [Required, Range(1, int.MaxValue)] int RoleId,
            [Required] bool CanViewBookings,
            [Required] bool CanEditBookings,
            [Required] bool CanViewPayments,
            [Required] bool CanEditPayments,
            [Required] bool CanCreateItinerary,
            [Required] bool CanUpdateItinerary,
            [Required] bool CanManageUsers,
            [Required] bool CanViewAnalytics,
            [Required] bool CanManageReviews
    );
}