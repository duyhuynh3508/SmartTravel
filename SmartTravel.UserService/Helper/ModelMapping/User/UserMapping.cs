using SmartTravel.Shared.Helper;
using SmartTravel.Shared.Entities;
using SmartTravel.UserService.Extension;
using SmartTravel.Shared.Extension.Enumerators;
using SmartTravel.Shared.Models;
using SmartTravel.Shared.Models.User;
using SmartTravel.Shared.Models.Role;

namespace SmartTravel.UserService.Helper.ModelMapping.User
{
    public interface IUserMapping : IBaseMapping<BaseEntity, BaseModel>
    {

    }
    public class UserMapping : IUserMapping
    {
        public BaseEntity ToEntity(BaseModel model)
        {
            BaseEntity entity = null;

            if (model is UserModel userModel) 
            {
                entity = new UserEntity()
                {
                    RoleId = (int)userModel.RoleId,
                    UserName = userModel.UserName,
                    Email = userModel.Email,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    DateOfBirth = userModel.DateOfBirth,
                    CreatedOn = DateTime.Now
                };
            }
            else if (model is CreateUserModel createUserRequest)
            {
                entity = new UserEntity()
                {
                    RoleId = (int)createUserRequest.RoleId,
                    UserName = createUserRequest.UserName,
                    Password = createUserRequest.Password,
                    PasswordHash = HashStringExtension.ToHashString(createUserRequest.Password),
                    Email = createUserRequest.Email,
                    FirstName = createUserRequest.FirstName,
                    LastName = createUserRequest.LastName,
                    DateOfBirth = createUserRequest.DateOfBirth,
                    CreatedOn = DateTime.Now
                };
            }

            return entity;
        }

        public BaseModel ToModel(BaseEntity entity)
        {
            BaseModel model = null;

            if(entity is UserEntity userEntity)
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
            else if (entity is RoleEntity roleEntity)
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
             .OfType<UserEntity>()
             .Select(entity => new UserModel()
             {
                 UserId = entity.UserId,
                 RoleId = (RoleEnum)entity.RoleId,
                 UserName = entity.UserName,
                 Email = entity.Email,
                 FirstName = entity.FirstName,
                 LastName = entity.LastName,
                 DateOfBirth = entity.DateOfBirth,
                 CreatedOn = entity.CreatedOn,
                 LastUpdatedOn = entity.LastUpdatedOn
             })
             .ToList();

            return models;
        }

        public BaseEntity ToEntity(BaseEntity entity, BaseModel model)
        {
            if (model is UpdateUserModel userModel && entity is UserEntity userEntity)
            {
                userEntity.RoleId = (int)userModel.RoleId;
                userEntity.FirstName = userModel.FirstName;
                userEntity.LastName = userModel.LastName;
                userEntity.DateOfBirth = userModel.DateOfBirth;
                userEntity.LastUpdatedOn = DateTime.Now;
            }

            return entity;
        }

        public BaseModel ToReferedModel(BaseModel existedModel, BaseEntity entity)
        {
            if (existedModel is UserModel userModel && entity is RoleEntity roleEntity)
            {
                userModel.Role = (RoleModel)ToModel(roleEntity);
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
                if (model is UserModel userModel)
                {
                    if (entities.OfType<RoleEntity>().Any())
                    {
                        var role = entities.OfType<RoleEntity>().FirstOrDefault(e => e.RoleId == (int)userModel.RoleId);

                        if (role == null) continue;

                        userModel.Role = (RoleModel)ToModel(role);
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
