using SmartTravel.BookingService.Models.HotelDetail;
using SmartTravel.Shared.Entities;
using SmartTravel.Shared.Helper;
using SmartTravel.Shared.Models;
using SmartTravel.Shared.Extension.Enumerators;
using System.Collections.Generic;
using System.Linq;
using SmartTravel.BookingService.Models.Booking;
using SmartTravel.BookingService.Models.CarRental;

namespace SmartTravel.BookingService.Helper.Mapping
{
    public interface IHotelDetailMapping : IBaseMapping<BaseEntity, BaseModel>
    {

    }
    public class HotelDetailMapping : IHotelDetailMapping
    {
        public BaseEntity ToEntity(BaseModel model)
        {
            BaseEntity entity = null;

            if (model is HotelDetailModel hotelDetailModel)
            {
                entity = new HotelDetailEntity()
                {
                    HotelDetailId = hotelDetailModel.HotelDetailId,
                    BookingId = hotelDetailModel.BookingId,
                    HotelDetailName = hotelDetailModel.HotelDetailName,
                    RoomTypeId = (int)hotelDetailModel.RoomTypeId,
                    CheckInDate = hotelDetailModel.CheckInDate,
                    CheckOutDate = hotelDetailModel.CheckOutDate,
                    HotelPrice = hotelDetailModel.HotelPrice
                };
            }
            else if (model is HotelDetailCreateModel createModel)
            {
                entity = new HotelDetailEntity()
                {
                    BookingId = createModel.BookingId,
                    HotelDetailName = createModel.HotelDetailName,
                    RoomTypeId = (int)createModel.RoomTypeId,
                    CheckInDate = createModel.CheckInDate,
                    CheckOutDate = createModel.CheckOutDate,
                    HotelPrice = createModel.HotelPrice
                };
            }

            return entity;
        }

        public BaseEntity ToEntity(BaseEntity entity, BaseModel model)
        {
            if (model is HotelDetailUpdateModel hotelDetailModel && entity is HotelDetailEntity hotelDetailEntity)
            {
                hotelDetailEntity.HotelDetailName = hotelDetailModel.HotelDetailName;
                hotelDetailEntity.RoomTypeId = (int)hotelDetailModel.RoomTypeId;
                hotelDetailEntity.CheckInDate = hotelDetailModel.CheckInDate;
                hotelDetailEntity.CheckOutDate = hotelDetailModel.CheckOutDate;
                hotelDetailEntity.HotelPrice = hotelDetailModel.HotelPrice;
            }

            return entity;
        }

        public BaseModel ToModel(BaseEntity entity)
        {
            BaseModel model = null;

            if (entity is HotelDetailEntity hotelDetailEntity)
            {
                model = new HotelDetailModel()
                {
                    HotelDetailId = hotelDetailEntity.HotelDetailId,
                    BookingId = hotelDetailEntity.BookingId,
                    HotelDetailName = hotelDetailEntity.HotelDetailName,
                    RoomTypeId = (RoomTypeEnum)hotelDetailEntity.RoomTypeId,
                    CheckInDate = hotelDetailEntity.CheckInDate,
                    CheckOutDate = hotelDetailEntity.CheckOutDate,
                    HotelPrice = hotelDetailEntity.HotelPrice
                };
            }
            else if (entity is BookingEntity bookingEntity)
            {
                BookingMapping bookingMapping = new BookingMapping();

                model = (BookingModel)bookingMapping.ToModel(bookingEntity);
            }

            return model;
        }

        public IEnumerable<BaseModel> ToListModels(IEnumerable<BaseEntity> entities)
        {
            BookingMapping bookingMapping = new BookingMapping();

            var models = entities
                .OfType<HotelDetailEntity>()
                .Select(entity => new HotelDetailModel()
                {
                    HotelDetailId = entity.HotelDetailId,
                    BookingId = entity.BookingId,
                    Booking = (BookingModel)bookingMapping.ToModel(entity),
                    HotelDetailName = entity.HotelDetailName,
                    RoomTypeId = (RoomTypeEnum)entity.RoomTypeId,
                    CheckInDate = entity.CheckInDate,
                    CheckOutDate = entity.CheckOutDate,
                    HotelPrice = entity.HotelPrice
                });

            return models;
        }

        public BaseModel ToReferedModel(BaseModel existedModel, BaseEntity entity)
        {

            if (existedModel is CarRentalModel model && entity is BookingEntity bookingEntity)
            {
                model.Booking = (BookingModel)ToModel(bookingEntity); ;
            }

            return existedModel;
        }

        public BaseModel ToReferedModel(BaseModel existedModel, IEnumerable<BaseEntity> entities)
        {
            throw new NotImplementedException();
        }

        public BaseModel ToReferedModel(BaseModel existedModel, IEnumerable<IEnumerable<BaseEntity>> collectionOfEntities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BaseModel> ToListReferedModels(IEnumerable<BaseModel> existedModels, IEnumerable<BaseEntity> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BaseModel> ToListReferedModels(IEnumerable<BaseModel> existedModels, IEnumerable<IEnumerable<BaseEntity>> collectionOfEntities)
        {
            throw new NotImplementedException();
        }
    }
}
