using SmartTravel.Shared.Extension.Enumerators;
using SmartTravel.Shared.Models;

namespace SmartTravel.BookingService.Models.CarRental
{
    public class CarRentalUpdateModel : BaseModel
    {
        public int CartRentalId { get; set; }
        public CarTypeEnum CarTypeId { get; set; }
        public DateTime CarRentalStart { get; set; }
        public DateTime CarRentalEnd { get; set; }
        public required string PickUpLocation { get; set; }
        public required string DropOffLocation { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
