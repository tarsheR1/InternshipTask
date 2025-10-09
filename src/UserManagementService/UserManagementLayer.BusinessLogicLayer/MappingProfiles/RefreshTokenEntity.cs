using AutoMapper;
using UserManagementService.BusinessLogicLayer.Models.Entities.Auth;
using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.BusinessLogicLayer.MappingProfiles
{
    public class RefreshTokenProfile : Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<RefreshTokenEntity, RefreshToken>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.Expires, opt => opt.MapFrom(src => src.Expires))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Revoked, opt => opt.MapFrom(src => src.Revoked));

            CreateMap<RefreshToken, RefreshTokenEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.Expires, opt => opt.MapFrom(src => src.Expires))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Revoked, opt => opt.MapFrom(src => src.Revoked))
                .ForMember(dest => dest.User, opt => opt.Ignore()); 
        }
    }
}