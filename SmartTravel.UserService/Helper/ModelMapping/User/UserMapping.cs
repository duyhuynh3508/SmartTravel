using SmartTravel.UserService.Entities;
using SmartTravel.UserService.Extension;
using SmartTravel.UserService.Models.User;

namespace SmartTravel.UserService.Helper.ModelMapping.User
{
    public static class UserMapping
    {
        public static UserEntity ToEntity (UserModel model)
        {
            UserEntity entity = new UserEntity()
            {
                UserId = model.UserId,
                RoleId = model.RoleId,
                UserName = model.UserName,
                Password = model.Password,
                PasswordHash = model.PasswordHash,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                CreatedOn = model.CreatedOn,
                LastUpdatedOn = model.LastUpdatedOn
            };

            return entity;
        }

        public static UserEntity ToEntity(CreateUserRequest model)
        {
            UserEntity entity = new UserEntity()
            {
                RoleId = model.RoleId,
                UserName = model.UserName,
                Password = model.Password,
                PasswordHash = HashStringExtension.ToHashString(model.Password),
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                CreatedOn = DateTime.Now
            };

            return entity;
        }
    }
}
