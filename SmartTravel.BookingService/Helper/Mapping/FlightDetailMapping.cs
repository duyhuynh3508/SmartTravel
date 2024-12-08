using SmartTravel.BookingService.Models.Booking;
using SmartTravel.BookingService.Models.CarRental;
using SmartTravel.BookingService.Models.FlightDetail;
using SmartTravel.Shared.Entities;
using SmartTravel.Shared.Helper;
using SmartTravel.Shared.Models;

namespace SmartTravel.BookingService.Helper.Mapping
{
    public interface IFlightDetailMapping : IBaseMapping<BaseEntity, BaseModel>
    {

    }
    public class FlightDetailMapping : IFlightDetailMapping
    {
        public BaseEntity ToEntity(BaseModel model)
        {
            BaseEntity entity = null;

            if (model is FlightDetailModel flightDetailModel)
            {
                entity = new FlightDetailEntity()
                {
                    FlightDetailId = flightDetailModel.FlightDetailId,
                    BookingId = flightDetailModel.BookingId,
                    FlightDateStart = flightDetailModel.FlightDateStart,
                    FlightDateEnd = flightDetailModel.FlightDateEnd,
                    DepartureLocation = flightDetailModel.DepartureLocation,
                    ArrivalLocation = flightDetailModel.ArrivalLocation,
                    FlightPrice = flightDetailModel.FlightPrice
                };
            }
            else if (model is FlightDetailCreateModel createModel)
            {
                entity = new FlightDetailEntity()
                {
                    BookingId = createModel.BookingId,
                    FlightDateStart = createModel.FlightDateStart,
                    FlightDateEnd = createModel.FlightDateEnd,
                    DepartureLocation = createModel.DepartureLocation,
                    ArrivalLocation = createModel.ArrivalLocation,
                    FlightPrice = createModel.FlightPrice
                };
            }

            return entity;
        }

        public BaseEntity ToEntity(BaseEntity entity, BaseModel model)
        {
            if (model is FlightDetailUpdateModel updateModel && entity is FlightDetailEntity flightDetailEntity)
            {
                flightDetailEntity.FlightDateStart = updateModel.FlightDateStart;
                flightDetailEntity.FlightDateEnd = updateModel.FlightDateEnd;
                flightDetailEntity.DepartureLocation = updateModel.DepartureLocation;
                flightDetailEntity.ArrivalLocation = updateModel.ArrivalLocation;
                flightDetailEntity.FlightPrice = updateModel.FlightPrice;
            }

            return entity;
        }

        public BaseModel ToModel(BaseEntity entity)
        {
            BaseModel model = null;

            if (entity is FlightDetailEntity flightDetailEntity)
            {
                model = new FlightDetailModel()
                {
                    FlightDetailId = flightDetailEntity.FlightDetailId,
                    BookingId = flightDetailEntity.BookingId,
                    FlightDateStart = flightDetailEntity.FlightDateStart,
                    FlightDateEnd = flightDetailEntity.FlightDateEnd,
                    DepartureLocation = flightDetailEntity.DepartureLocation,
                    ArrivalLocation = flightDetailEntity.ArrivalLocation,
                    FlightPrice = flightDetailEntity.FlightPrice
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
                .OfType<FlightDetailEntity>()
                .Select(entity => new FlightDetailModel()
                {
                    FlightDetailId = entity.FlightDetailId,
                    BookingId = entity.BookingId,
                    Booking = (BookingModel)bookingMapping.ToModel(entity),
                    FlightDateStart = entity.FlightDateStart,
                    FlightDateEnd = entity.FlightDateEnd,
                    DepartureLocation = entity.DepartureLocation,
                    ArrivalLocation = entity.ArrivalLocation,
                    FlightPrice = entity.FlightPrice
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
