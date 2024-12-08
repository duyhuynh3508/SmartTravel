using System.Linq;
using SmartTravel.BookingService.Models;
using SmartTravel.BookingService.Models.Booking;
using SmartTravel.Shared.Entities;
using SmartTravel.Shared.Extension.Enumerators;
using SmartTravel.Shared.Helper;
using SmartTravel.Shared.Models;

namespace SmartTravel.BookingService.Helper.Mapping
{
    public interface IBookingMapping : IBaseMapping<BaseEntity, BaseModel>
    {
        IEnumerable<BaseModel> MapToMissingPropertyModel(IEnumerable<BaseModel> existedModels, IEnumerable<BaseModel> modelsToMap);
    }
    public class BookingMapping : IBookingMapping
    {
        public BaseEntity ToEntity(BaseModel model)
        {
            BaseEntity entity = null;

            if(model is BookingModel bookingModel)
            {
                entity = new BookingEntity()
                {
                    BookingId = bookingModel.BookingId,
                    UserId = bookingModel.UserId,
                    BookingStatusId = (int)bookingModel.BookingStatusId,
                    BookingDate = bookingModel.BookingDate
                };
            }

            if (model is BookingCreateModel createModel)
            {
                entity = new BookingEntity()
                {
                    UserId = createModel.UserId,
                    BookingStatusId = (int)createModel.BookingStatusId,
                    BookingDate = createModel.BookingDate
                };
            }

            return entity;
        }

        public BaseEntity ToEntity(BaseEntity entity, BaseModel model)
        {
            if (model is BookingUpdateModel bookingModel && entity is BookingEntity bookingEntity)
            {
                bookingEntity.BookingId = bookingModel.BookingId;
                bookingEntity.BookingStatusId = (int)bookingModel.BookingStatusId;
                bookingEntity.BookingDate = bookingModel.BookingDate;
            }

            return entity;
        }

        public BaseModel ToModel(BaseEntity entity)
        {
            BaseModel model = null;
            if (entity is BookingEntity bookingEntity)
            {
                model = new BookingModel()
                {
                    BookingId = bookingEntity.BookingId,
                    UserId = bookingEntity.UserId,
                    BookingStatusId = (BookingStatusEnum)bookingEntity.BookingStatusId,
                    BookingDate = bookingEntity.BookingDate
                };
            }

            if (entity is UserEntity userEntity)
            {
                model = new UserModel()
                {
                    UserId = userEntity.UserId,
                    RoleId = (RoleEnum)userEntity.RoleId,
                    UserName = userEntity.UserName,
                    Email = userEntity.Email,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    DateOfBirth = userEntity.DateOfBirth,
                    CreatedOn = userEntity.CreatedOn,
                    LastUpdatedOn = userEntity.LastUpdatedOn
                };
            }

            return model;
        } 

        public IEnumerable<BaseModel> ToListModels(IEnumerable<BaseEntity> entities)
        {
            IEnumerable<BaseModel> models = null;

            if (entities.OfType<BookingEntity>().Count() > 0)
            {
                models = entities
                .OfType<BookingEntity>()
                .Select(entity => new BookingModel()
                {
                    BookingId = entity.BookingId,
                    UserId = entity.UserId,
                    User = (UserModel)ToModel(entity),
                    BookingStatusId = (BookingStatusEnum)entity.BookingStatusId,
                    BookingDate = entity.BookingDate
                });
            }

            return models;
        }

        public BaseModel ToReferedModel(BaseModel existedModel, BaseEntity entity)
        {

            if (existedModel is BookingModel model && entity is UserEntity userEntity)
            {
                model.User = (UserModel)ToModel(userEntity); ;
            }

            return existedModel;
        }

        public IEnumerable<BaseModel> ToListReferedModels(IEnumerable<BaseModel> existedModels, IEnumerable<BaseEntity> entities)
        {

            if (entities.OfType<UserEntity>().Any())
            {
                foreach(var model in existedModels)
                {
                    if (model is BookingModel bookingModel)
                    {
                        var userEntity = entities.OfType<UserEntity>().FirstOrDefault(u => u.UserId ==  bookingModel.UserId);

                        if (userEntity == null)
                            continue;

                        bookingModel.User = (UserModel)ToReferedModel(bookingModel, userEntity);
                    }
                }
            }

            return existedModels;
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

        public IEnumerable<BaseModel> MapToMissingPropertyModel(IEnumerable<BaseModel> existedModels, IEnumerable<BaseModel> modelsToMap)
        {
            foreach (var model in existedModels)
            {
                if (model is BookingModel bookingModel)
                {
                    var userModel = modelsToMap.OfType<UserModel>().FirstOrDefault(u => u.UserId == bookingModel.UserId);

                    if (userModel == null)
                        continue;

                    bookingModel.User = userModel;
                }
            }

            return existedModels;
        }
    }
}
