using AutoMapper;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.BusinessLogicLayer.Services.ExternalServices.Mapping
{
    public class RolePermissionProfile : Profile
    {
        public RolePermissionProfile()
        {
            CreateMap<RolePermission, RolePermissionEntity>()
                .ForMember(dest => dest.Permission, opt => opt.MapFrom(src => src.Permission))
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Permission, opt => opt.MapFrom(src => src.Permission))
                .ForMember(dest => dest.Role, opt => opt.Ignore());
        }
    }
}
