using AutoMapper;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request.Auth;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request.Users;

namespace UserManagementService.BusinessLogicLayer.Mapping
{
    public class UserRequestProfile : Profile
    {
        public UserRequestProfile()
        {
            CreateMap<RegisterUserRequestDto, CreateUserRequestDto>();

            CreateMap<CreateUserRequestDto, RegisterUserRequestDto>();
        }
    }
}
