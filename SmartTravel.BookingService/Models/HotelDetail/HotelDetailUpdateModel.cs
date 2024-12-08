using SmartTravel.Shared.Extension.Enumerators;
using SmartTravel.Shared.Models;

namespace SmartTravel.BookingService.Models.HotelDetail
{
    public class HotelDetailUpdateModel : BaseModel
    {
        public int HotelDetailId { get; set; }
        public required string HotelDetailName { get; set; }
        public RoomTypeEnum RoomTypeId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal HotelPrice { get; set; }
    }
}
