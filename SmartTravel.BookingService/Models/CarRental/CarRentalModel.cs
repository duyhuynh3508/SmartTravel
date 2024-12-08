using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SmartTravel.Shared.Models;
using SmartTravel.Shared.Extension.Enumerators;
using SmartTravel.BookingService.Models.Booking;

namespace SmartTravel.BookingService.Models.CarRental
{
    public class CarRentalModel : BaseModel
    {
        public int CarRentalId { get; set; }
        public int BookingId { get; set; }
        public BookingModel Booking { get; set; }
        public CarTypeEnum CarTypeId { get; set; }
        public DateTime CarRentalStart { get; set; }
        public DateTime CarRentalEnd { get; set; }
        public required string PickUpLocation { get; set; }
        public required string DropOffLocation { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
