using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTravel.Shared.Entities
{
    public class HotelDetailEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelDetailId { get; set; }
        [Required]
        public int BookingId { get; set; }
        [Required]
        public required string HotelDetailName { get; set; }
        [Required]
        public int RoomTypeId { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        [Required]
        public decimal HotelPrice { get; set; }
    }
}
