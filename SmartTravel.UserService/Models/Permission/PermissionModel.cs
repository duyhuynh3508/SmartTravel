using SmartTravel.Shared.Models;
using SmartTravel.Shared.Extension.Enumerators;
using SmartTravel.Shared.Models.Role;

namespace SmartTravel.Shared.Models.Permission
{
    public class PermissionModel : BaseModel
    {
        public int PermissionId { get; set; }
        public RoleEnum RoleId { get; set; }
        public RoleModel Role { get; set; }
        public bool CanViewBookings { get; set; }
        public bool CanEditBookings { get; set; }
        public bool CanViewPayments { get; set; }
        public bool CanEditPayments { get; set; }
        public bool CanCreateItinerary { get; set; }
        public bool CanUpdateItinerary { get; set; }
        public bool CanManageUsers { get; set; }
        public bool CanViewAnalytics { get; set; }
        public bool CanManageReviews { get; set; }
    }
}