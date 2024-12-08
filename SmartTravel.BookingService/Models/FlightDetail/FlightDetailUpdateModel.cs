using SmartTravel.Shared.Models;

namespace SmartTravel.BookingService.Models.FlightDetail
{
    public class FlightDetailUpdateModel : BaseModel
    {
        public int FlightDetailId { get; set; }
        public DateTime FlightDateStart { get; set; }
        public DateTime FlightDateEnd { get; set; }
        public required string DepartureLocation { get; set; }
        public required string ArrivalLocation { get; set; }
        public decimal FlightPrice { get; set; }
    }
}
