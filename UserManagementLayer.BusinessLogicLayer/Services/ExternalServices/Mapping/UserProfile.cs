using AutoMapper;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.BusinessLogicLayer.Services.ExternalServices.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserEntity>()
                .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles))
                .ReverseMap()
                .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles));

            CreateMap<UserRole, UserRoleEntity>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Role, RoleEntity>()
                .ForMember(dest => dest.RolePermissions, opt => opt.MapFrom(src => src.RolePermissions))
                .ReverseMap()
                .ForMember(dest => dest.RolePermissions, opt => opt.MapFrom(src => src.RolePermissions));

            CreateMap<RolePermission, RolePermissionEntity>()
                .ForMember(dest => dest.Permission, opt => opt.MapFrom(src => src.Permission))
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Permission, opt => opt.MapFrom(src => src.Permission))
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<Permission, PermissionEntity>()
                .ReverseMap();

        }
    }
}
