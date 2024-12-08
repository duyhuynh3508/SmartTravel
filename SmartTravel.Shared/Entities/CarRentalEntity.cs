using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTravel.Shared.Entities
{
    public class CarRentalEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarRentalId { get; set; }
        [Required]
        public int BookingId { get; set; }
        [Required]
        public int CarTypeId { get; set; }
        [Required]
        public DateTime CarRentalStart { get; set; }
        [Required]
        public DateTime CarRentalEnd { get; set; }
        [Required]
        public required string PickUpLocation { get; set; }
        [Required]
        public required string DropOffLocation { get; set; }
        [Required]
        public decimal PricePerDay { get; set; }
    }
}
