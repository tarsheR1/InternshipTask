using AutoMapper;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.DataAccessLayer.Entities.Role;

namespace UserManagementService.BusinessLogicLayer.Mapping
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
