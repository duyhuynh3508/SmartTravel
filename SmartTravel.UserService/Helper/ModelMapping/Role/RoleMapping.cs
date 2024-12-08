using SmartTravel.Shared.Helper;
using SmartTravel.Shared.Entities;
using SmartTravel.Shared.Extension.Enumerators;
using SmartTravel.Shared.Models;
using SmartTravel.Shared.Models.Role;

namespace SmartTravel.UserService.Helper.ModelMapping.Role
{
    public interface IRoleMapping : IBaseMapping<BaseEntity, BaseModel>
    {


    }
    public class RoleMapping : IRoleMapping
    {
        public BaseEntity ToEntity(BaseModel model)
        {
            BaseEntity entity = null;

            if(model is RoleModel roleModel)
            {
                entity = new RoleEntity()
                {
                    RoleId = (int)roleModel.RoleId,
                    RoleName = roleModel.RoleName,
                };
            }
            else if(model is CreateRoleModel createRoleModel)
            {
                entity = new RoleEntity()
                {
                    RoleId = (int)createRoleModel.RoleId,
                    RoleName = createRoleModel.RoleName,
                };
            }

            return entity;
        }

        public BaseEntity ToEntity(BaseEntity entity, BaseModel model)
        {
            if(model is RoleModel roleModel && entity is RoleEntity roleEntity)
            {
                roleEntity.RoleId = (int)roleModel.RoleId;
                roleEntity.RoleName = roleModel.RoleName;
            }
            return entity;
        }

        public BaseModel ToModel(BaseEntity entity)
        {
            BaseModel model = null;
            if(entity is RoleEntity roleEntity)
            {
                model = new RoleModel()
                {
                    RoleId = (RoleEnum)roleEntity.RoleId,
                    RoleName = roleEntity.RoleName,
                };
            }
            return model;
        }

        public IEnumerable<BaseModel> ToListModels(IEnumerable<BaseEntity> entities)
        {
            var models = entities
             .OfType<RoleEntity>()
             .Select(entity => new RoleModel()
             {
                 RoleId = (RoleEnum)entity.RoleId,
                 RoleName = entity.RoleName,
             })
             .ToList();

            return models;
        }

        public BaseModel ToReferedModel(BaseModel existedModel, BaseEntity entity)
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
