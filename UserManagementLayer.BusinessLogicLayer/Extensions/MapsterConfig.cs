using Mapster;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.BusinessLogicLayer.Services.ExternalServices;
using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.BusinessLogicLayer.Extensions
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<User, UserEntity>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PasswordHash, src => src.PasswordHash)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.MiddleName, src => src.MiddleName)
                .Map(dest => dest.Phone, src => src.Phone)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.UserRoles, src => src.UserRoles.Adapt<ICollection<UserRoleEntity>>());

            TypeAdapterConfig<UserEntity, User>.NewConfig()
                .Map(dest => dest.UserRoles, src => src.UserRoles.Adapt<ICollection<UserRole>>());


            TypeAdapterConfig<UserRoleEntity, UserRole>.NewConfig()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.RoleId, src => src.RoleId)
                .Map(dest => dest.User, src => src.User.Adapt<User>())
                .Map(dest => dest.Role, src => src.Role.Adapt<Role>());

            TypeAdapterConfig<RoleEntity, Role>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.UserRoles, src => src.UserRoles.Adapt<ICollection<UserRole>>())
                .Map(dest => dest.RolePermissions, src => src.RolePermissions.Adapt<ICollection<RolePermission>>());

            TypeAdapterConfig<RolePermissionEntity, RolePermission>.NewConfig()
                .Map(dest => dest.RoleId, src => src.RoleId)
                .Map(dest => dest.PermissionId, src => src.PermissionId)
                .Map(dest => dest.Role, src => src.Role.Adapt<Role>())
                .Map(dest => dest.Permission, src => src.Permission.Adapt<Permission>());
        }
    }
}
