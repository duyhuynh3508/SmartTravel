using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SmartTravel.Shared.Models;
using SmartTravel.BookingService.Models.Booking;

namespace SmartTravel.BookingService.Models.FlightDetail
{
    public class FlightDetailModel : BaseModel
    {
        public int FlightDetailId { get; set; }
        public int BookingId { get; set; }
        public BookingModel Booking { get; set; }
        public DateTime FlightDateStart { get; set; }
        public DateTime FlightDateEnd { get; set; }
        public required string DepartureLocation { get; set; }
        public required string ArrivalLocation { get; set; }
        public decimal FlightPrice { get; set; }
    }
}
