using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTravel.Shared.Entities
{
    public class PermissionEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionId { get; set; }
        public int RoleId { get; set; }

        [Required]
        public bool CanViewBookings { get; set; }
        [Required]
        public bool CanEditBookings { get; set; }
        [Required]
        public bool CanViewPayments { get; set; }
        [Required]
        public bool CanEditPayments { get; set; }
        [Required]
        public bool CanCreateItinerary { get; set; }
        [Required]
        public bool CanUpdateItinerary { get; set; }
        [Required]
        public bool CanManageUsers { get; set; }
        [Required]
        public bool CanViewAnalytics { get; set; }
        [Required]
        public bool CanManageReviews { get; set; }
    }
}
