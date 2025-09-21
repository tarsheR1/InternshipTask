
using AutoMapper;
using EventManagementService.Application.DTO;
using EventManagementService.Application.UseCases.Сommands.Events;
using EventManagementService.Domain.Entities;

namespace EventManagementService.Application.Mapping
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            // Event -> DTO
            CreateMap<EventEntity, EventDto>()
                .ForMember(dest => dest.Categories,
                    opt => opt.MapFrom(src => src.Categories))
                .ForMember(dest => dest.IsActive,
                    opt => opt.MapFrom(src => src.IsActive ? "Active" : "Inactive"));

            CreateMap<CreateEventCommand, EventEntity>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Categories,
                    opt => opt.Ignore());

            CreateMap<UpdateEventCommand, EventEntity>()
                .ForMember(dest => dest.Categories,
                    opt => opt.Ignore());
        }
    }
}
