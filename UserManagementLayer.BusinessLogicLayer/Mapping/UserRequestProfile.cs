using AutoMapper;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request;

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
