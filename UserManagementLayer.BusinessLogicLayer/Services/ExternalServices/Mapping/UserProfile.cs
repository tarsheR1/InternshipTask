using AutoMapper;
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
        }
    }
}
