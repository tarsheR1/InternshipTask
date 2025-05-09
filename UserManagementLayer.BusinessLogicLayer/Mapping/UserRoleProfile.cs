using AutoMapper;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.DataAccessLayer.Entities.Relations;

namespace UserManagementService.BusinessLogicLayer.Mapping
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRole, UserRoleEntity>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}
