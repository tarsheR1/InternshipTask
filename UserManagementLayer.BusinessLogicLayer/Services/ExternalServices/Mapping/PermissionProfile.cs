using AutoMapper;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.BusinessLogicLayer.Services.ExternalServices.Mapping
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, PermissionEntity>()
                .ReverseMap();
        }
    }
}
