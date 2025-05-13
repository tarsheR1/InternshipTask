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
                    .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions))
                    .ReverseMap()
                    .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions));
        }
    }
}
