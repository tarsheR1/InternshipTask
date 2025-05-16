using AutoMapper;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.DataAccessLayer.Entities.Users;

namespace UserManagementService.BusinessLogicLayer.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserEntity>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles))
                .ReverseMap()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));
        }
    }
}
