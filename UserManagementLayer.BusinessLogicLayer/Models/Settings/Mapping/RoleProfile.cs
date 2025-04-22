using AutoMapper;
using UserManagementService.DataAccessLayer.Entities;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

namespace UserManagementService.BusinessLogicLayer.Models.Settings.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile() 
        {
            CreateMap<Role, RoleEntity>()
                    .ForMember(dest => dest.RolePermissions, opt => opt.MapFrom(src => src.RolePermissions))
                    .ReverseMap()
                    .ForMember(dest => dest.RolePermissions, opt => opt.MapFrom(src => src.RolePermissions));
        }
    }
}
