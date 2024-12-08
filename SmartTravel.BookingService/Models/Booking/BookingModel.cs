using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SmartTravel.Shared.Models;
using SmartTravel.Shared.Extension.Enumerators;

namespace SmartTravel.BookingService.Models.Booking
{
    public class BookingModel : BaseModel
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public BookingStatusEnum BookingStatusId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
