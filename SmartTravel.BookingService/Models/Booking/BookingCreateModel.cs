using SmartTravel.Shared.Extension.Enumerators;
using SmartTravel.Shared.Models;

namespace SmartTravel.BookingService.Models.Booking
{
    public class BookingCreateModel : BaseModel
    {
        public int UserId { get; set; }
        public BookingStatusEnum BookingStatusId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
