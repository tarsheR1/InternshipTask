using AutoMapper;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.DataAccessLayer.Entities.Role;

namespace UserManagementService.BusinessLogicLayer.Models.Settings.Mapping
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
