using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTravel.Shared.Entities
{
    public class BookingStatusEnity : BaseEntity
    {
        [Key]
        public int BookingStatusId { get; set; }
        [Required]
        public required string BookingStatusName { get; set; }
    }
}
