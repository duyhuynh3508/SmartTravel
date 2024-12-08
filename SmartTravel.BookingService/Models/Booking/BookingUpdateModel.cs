using SmartTravel.Shared.Extension.Enumerators;
using SmartTravel.Shared.Models;

namespace SmartTravel.BookingService.Models.Booking
{
    public class BookingUpdateModel : BaseModel
    {
        public int BookingId { get; set; }
        public BookingStatusEnum BookingStatusId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
