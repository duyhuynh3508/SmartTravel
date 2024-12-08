using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTravel.Shared.Entities
{
    public class BookingEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookingStatusId { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
    }
}
