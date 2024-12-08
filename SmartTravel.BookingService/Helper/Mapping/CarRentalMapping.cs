using SmartTravel.BookingService.Models.Booking;
using SmartTravel.BookingService.Models.CarRental;
using SmartTravel.Shared.Entities;
using SmartTravel.Shared.Extension.Enumerators;
using SmartTravel.Shared.Helper;
using SmartTravel.Shared.Models;

namespace SmartTravel.BookingService.Helper.Mapping
{
    public interface ICarRentalMapping : IBaseMapping<BaseEntity, BaseModel>
    {

    }
    public class CarRentalMapping : ICarRentalMapping
    {
        public BaseEntity ToEntity(BaseModel model)
        {
            BaseEntity entity = null;
            if (model is CarRentalModel carRentalModel)
            {
                entity = new CarRentalEntity()
                {
                    CarRentalId = carRentalModel.CarRentalId,
                    BookingId = carRentalModel.BookingId,
                    CarTypeId = (int)carRentalModel.CarTypeId,
                    CarRentalStart = carRentalModel.CarRentalStart,
                    CarRentalEnd = carRentalModel.CarRentalEnd,
                    PickUpLocation = carRentalModel.PickUpLocation,
                    DropOffLocation = carRentalModel.DropOffLocation,
                    PricePerDay = carRentalModel.PricePerDay
                };
            }
            else if (model is CarRentalCreateModel createModel) 
            {
                entity = new CarRentalEntity()
                {
                    BookingId = createModel.BookingId,
                    CarTypeId = (int)createModel.CarTypeId,
                    CarRentalStart = createModel.CarRentalStart,
                    CarRentalEnd = createModel.CarRentalEnd,
                    PickUpLocation = createModel.PickUpLocation,
                    DropOffLocation = createModel.DropOffLocation,
                    PricePerDay = createModel.PricePerDay
                };
            }

            return entity;
        }

        public BaseEntity ToEntity(BaseEntity entity, BaseModel model)
        {
            if (model is CarRentalUpdateModel carRentalModel && entity is CarRentalEntity carRentalEntity)
            {
                carRentalEntity.CarTypeId = (int)carRentalModel.CarTypeId;
                carRentalEntity.CarRentalStart = carRentalModel.CarRentalStart;
                carRentalEntity.CarRentalEnd = carRentalModel.CarRentalEnd;
                carRentalEntity.PickUpLocation = carRentalModel.PickUpLocation;
                carRentalEntity.DropOffLocation = carRentalModel.DropOffLocation;
                carRentalEntity.PricePerDay = carRentalModel.PricePerDay;
            }

            return entity;
        }

        public BaseModel ToModel(BaseEntity entity)
        {
            BaseModel model = null;
            if (entity is CarRentalEntity carRentalEntity)
            {
                model = new CarRentalModel()
                {
                    CarRentalId = carRentalEntity.CarRentalId,
                    BookingId = carRentalEntity.BookingId,
                    CarTypeId = (CarTypeEnum)carRentalEntity.CarTypeId,
                    CarRentalStart = carRentalEntity.CarRentalStart,
                    CarRentalEnd = carRentalEntity.CarRentalEnd,
                    PickUpLocation = carRentalEntity.PickUpLocation,
                    DropOffLocation = carRentalEntity.DropOffLocation,
                    PricePerDay = carRentalEntity.PricePerDay
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
                .OfType<CarRentalEntity>()
                .Select(carRentalEntity => new CarRentalModel() 
                {
                    CarRentalId = carRentalEntity.CarRentalId,
                    BookingId = carRentalEntity.BookingId,
                    Booking = (BookingModel)bookingMapping.ToModel(carRentalEntity),
                    CarTypeId = (CarTypeEnum)carRentalEntity.CarTypeId,
                    CarRentalStart = carRentalEntity.CarRentalStart,
                    CarRentalEnd = carRentalEntity.CarRentalEnd,
                    PickUpLocation = carRentalEntity.PickUpLocation,
                    DropOffLocation = carRentalEntity.DropOffLocation,
                    PricePerDay = carRentalEntity.PricePerDay
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

        public IEnumerable<BaseModel> ToListReferedModels(IEnumerable<BaseModel> existedModels, IEnumerable<BaseEntity> entities)
        {
            throw new NotImplementedException();
        }

        public BaseModel ToReferedModel(BaseModel existedModel, IEnumerable<BaseEntity> entities)
        {
            throw new NotImplementedException();
        }

        public BaseModel ToReferedModel(BaseModel existedModel, IEnumerable<IEnumerable<BaseEntity>> collectionOfEntities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BaseModel> ToListReferedModels(IEnumerable<BaseModel> existedModels, IEnumerable<IEnumerable<BaseEntity>> collectionOfEntities)
        {
            throw new NotImplementedException();
        }
    }
}
