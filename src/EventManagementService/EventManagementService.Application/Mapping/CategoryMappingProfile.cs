using AutoMapper;
using EventManagementService.Application.DTO;
using EventManagementService.Application.UseCases.Сommands.Categories;
using EventManagementService.Domain.Entities;

namespace EventManagementService.Application.Mapping
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryEntity, CategoryDto>()
                .ForMember(dest => dest.Events.Count,
                    opt => opt.MapFrom(src => src.Events.Count));

            CreateMap<CreateCategoryCommand, CategoryEntity>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Events,
                    opt => opt.Ignore());

            CreateMap<UpdateCategoryCommand, CategoryEntity>()
                .ForMember(dest => dest.Events,
                    opt => opt.Ignore());
        }
    }
}
