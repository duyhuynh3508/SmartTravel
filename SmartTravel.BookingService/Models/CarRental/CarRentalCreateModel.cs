﻿using SmartTravel.Shared.Extension.Enumerators;
using SmartTravel.Shared.Models;

namespace SmartTravel.BookingService.Models.CarRental
{
    public class CarRentalCreateModel : BaseModel
    {
        public int CarRentalId { get; set; }
        public int BookingId { get; set; }
        public CarTypeEnum CarTypeId { get; set; }
        public DateTime CarRentalStart { get; set; }
        public DateTime CarRentalEnd { get; set; }
        public required string PickUpLocation { get; set; }
        public required string DropOffLocation { get; set; }
        public decimal PricePerDay { get; set; }
    }
}