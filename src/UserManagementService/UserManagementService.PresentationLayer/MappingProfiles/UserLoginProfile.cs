using AutoMapper;
using UserManagementService.BusinessLogicLayer.Commands;
using UserManagementService.PresentationLayer.DTO.Request;

namespace UserManagementService.PresentationLayer.MappingProfiles
{
    public class UserLoginProfile : Profile
    {
        public UserLoginProfile()
        {
            CreateMap<LoginRequestDto, UserLoginCommand>();
        }
    }
}
