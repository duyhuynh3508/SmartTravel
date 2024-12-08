using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SmartTravel.Shared.Models;
using SmartTravel.Shared.Extension.Enumerators;
using SmartTravel.BookingService.Models.Booking;

namespace SmartTravel.BookingService.Models.HotelDetail
{
    public class HotelDetailModel : BaseModel
    {
        public int HotelDetailId { get; set; }
        public int BookingId { get; set; }
        public BookingModel Booking { get; set; }
        public required string HotelDetailName { get; set; }
        public RoomTypeEnum RoomTypeId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal HotelPrice { get; set; }
    }
}
