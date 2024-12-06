using SmartTravel.UserService.Entities;
using SmartTravel.UserService.Models.Permission;

namespace SmartTravel.UserService.Helper.ModelMapping.Permission
{
    public static class PermissionMapping
    {
        public static PermissionEntity ToEntity(PermissionModel model)
        {
            PermissionEntity permission = new PermissionEntity()
            {
                RoleId = model.RoleId,
                CanCreateItinerary = model.CanCreateItinerary,
                CanEditBookings = model.CanEditBookings,
                CanEditPayments = model.CanEditPayments,
                CanManageReviews = model.CanManageReviews,
                CanManageUsers = model.CanManageUsers,
                CanUpdateItinerary = model.CanUpdateItinerary,
                CanViewAnalytics = model.CanViewAnalytics,
                CanViewBookings = model.CanViewBookings,
                CanViewPayments = model.CanViewPayments,
            };

            return permission;
        }
    }
}
