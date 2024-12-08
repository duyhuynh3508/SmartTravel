using SmartTravel.Shared.Helper;
using SmartTravel.Shared.Entities;
using SmartTravel.Shared.Extension.Enumerators;
using SmartTravel.Shared.Models;
using SmartTravel.Shared.Models.Permission;
using SmartTravel.Shared.Models.Role;

namespace SmartTravel.UserService.Helper.ModelMapping.Permission
{
    public interface IPermissionMapping : IBaseMapping<BaseEntity, BaseModel>
    {

    }

    public class PermissionMapping : IPermissionMapping
    {
        public BaseEntity ToEntity(BaseModel model)
        {
            BaseEntity entity = null;

            if (model is PermissionModel permissionModel)
            {
                entity = new PermissionEntity()
                {
                    RoleId = (int)permissionModel.RoleId,
                    CanCreateItinerary = permissionModel.CanCreateItinerary,
                    CanEditBookings = permissionModel.CanEditBookings,
                    CanEditPayments = permissionModel.CanEditPayments,
                    CanManageReviews = permissionModel.CanManageReviews,
                    CanManageUsers = permissionModel.CanManageUsers,
                    CanUpdateItinerary = permissionModel.CanUpdateItinerary,
                    CanViewAnalytics = permissionModel.CanViewAnalytics,
                    CanViewBookings = permissionModel.CanViewBookings,
                    CanViewPayments = permissionModel.CanViewPayments
                };
            }
            if (model is PermissionCreateModel permissionCreateModel)
            {
                entity = new PermissionEntity()
                {
                    RoleId = (int)permissionCreateModel.RoleId,
                    CanCreateItinerary = permissionCreateModel.CanCreateItinerary,
                    CanEditBookings = permissionCreateModel.CanEditBookings,
                    CanEditPayments = permissionCreateModel.CanEditPayments,
                    CanManageReviews = permissionCreateModel.CanManageReviews,
                    CanManageUsers = permissionCreateModel.CanManageUsers,
                    CanUpdateItinerary = permissionCreateModel.CanUpdateItinerary,
                    CanViewAnalytics = permissionCreateModel.CanViewAnalytics,
                    CanViewBookings = permissionCreateModel.CanViewBookings,
                    CanViewPayments = permissionCreateModel.CanViewPayments
                };
            }

            return entity;
        }

        public BaseEntity ToEntity(BaseEntity entity, BaseModel model)
        {
            if (model is PermissionUpdateModel permissionModel && entity is PermissionEntity permissionEntity) 
            {
                permissionEntity.PermissionId = permissionModel.PermissionId;
                permissionEntity.RoleId = (int)permissionModel.RoleId;
                permissionEntity.CanCreateItinerary = permissionModel.CanCreateItinerary;
                permissionEntity.CanEditBookings = permissionModel.CanEditBookings;
                permissionEntity.CanEditPayments = permissionModel.CanEditPayments;
                permissionEntity.CanManageReviews = permissionModel.CanManageReviews;
                permissionEntity.CanManageUsers = permissionModel.CanManageUsers;
                permissionEntity.CanUpdateItinerary = permissionModel.CanUpdateItinerary;
                permissionEntity.CanViewAnalytics = permissionModel.CanViewAnalytics;
                permissionEntity.CanViewBookings = permissionModel.CanViewBookings;
                permissionEntity.CanViewPayments = permissionModel.CanViewPayments;
            }
            return entity;
        }

        public BaseModel ToModel(BaseEntity entity)
        {
            BaseModel model = null;

            if (entity is PermissionEntity permissionEntity) 
            {
                model = new PermissionModel()
                {
                    PermissionId = permissionEntity.PermissionId,
                    RoleId = (RoleEnum)permissionEntity.RoleId,
                    CanCreateItinerary = permissionEntity.CanCreateItinerary,
                    CanEditBookings = permissionEntity.CanEditBookings,
                    CanEditPayments = permissionEntity.CanEditPayments,
                    CanManageReviews = permissionEntity.CanManageReviews,
                    CanManageUsers = permissionEntity.CanManageUsers,
                    CanUpdateItinerary = permissionEntity.CanUpdateItinerary,
                    CanViewAnalytics = permissionEntity.CanViewAnalytics,
                    CanViewBookings = permissionEntity.CanViewBookings,
                    CanViewPayments = permissionEntity.CanViewPayments
                };
            }

            if (entity is RoleEntity roleEntity)
            {
                model = new RoleModel()
                {
                    RoleId = (RoleEnum)roleEntity.RoleId,
                    RoleName = roleEntity.RoleName
                };
            }

            return model;
        }

        public IEnumerable<BaseModel> ToListModels(IEnumerable<BaseEntity> entities)
        {
            var models = entities
             .OfType<PermissionEntity>()
             .Select(entity => new PermissionModel()
             {
                 PermissionId = entity.PermissionId,
                 RoleId = (RoleEnum)entity.RoleId,
                 CanCreateItinerary = entity.CanCreateItinerary,
                 CanEditBookings = entity.CanEditBookings,
                 CanEditPayments = entity.CanEditPayments,
                 CanManageReviews = entity.CanManageReviews,
                 CanManageUsers = entity.CanManageUsers,
                 CanUpdateItinerary = entity.CanUpdateItinerary,
                 CanViewAnalytics = entity.CanViewAnalytics,
                 CanViewBookings = entity.CanViewBookings,
                 CanViewPayments = entity.CanViewPayments
             })
             .ToList();

            return models;
        }

        public BaseModel ToReferedModel(BaseModel existedModel, BaseEntity entity)
        {
            if (existedModel is PermissionModel permissionModel)
            {
                if (entity is RoleEntity roleEntity)
                {
                    permissionModel.Role = (RoleModel)ToModel(roleEntity);
                }
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
            foreach (var model in existedModels)
            {
                if (model is PermissionModel permissionMode)
                {
                    if (entities.OfType<RoleEntity>().Any())
                    {
                        var role = entities.OfType<RoleEntity>().FirstOrDefault(e => e.RoleId == (int)permissionMode.RoleId);

                        if (role == null) continue;

                        permissionMode.Role = (RoleModel)ToModel(role);
                    }
                }
            }

            return existedModels;
        }

        public IEnumerable<BaseModel> ToListReferedModels(IEnumerable<BaseModel> existedModels, IEnumerable<IEnumerable<BaseEntity>> collectionOfEntities)
        {
            throw new NotImplementedException();
        }
    }
}
