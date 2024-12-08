using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTravel.Shared.Entities
{
    public class FlightDetailEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FlightDetailId { get; set; }
        [Required]
        public int BookingId { get; set; }
        [Required]
        public DateTime FlightDateStart { get; set; }
        [Required]
        public DateTime FlightDateEnd { get; set; }
        [Required]
        public required string DepartureLocation { get; set; }
        [Required]
        public required string ArrivalLocation { get; set; }
        [Required]
        public decimal FlightPrice { get; set; }
    }
}
