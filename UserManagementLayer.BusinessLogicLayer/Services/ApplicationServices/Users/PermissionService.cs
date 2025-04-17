using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.DataAccessLayer.Interfaces;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Users
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AutoMapper.IMapper _mapper;

        public PermissionService(IUnitOfWork unitOfWork, AutoMapper.IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<Permission>> GetAllPermissionsAsync(CancellationToken cancellationToken)
        {
            var permissions = await _unitOfWork.Permissions.GetAll(cancellationToken);
            
            return _mapper.Map<List<Permission>>(permissions);
        }

        public async Task<Permission> GetById(int id, CancellationToken cancellationToken)
        {
            var permissionEntity = await _unitOfWork.Permissions.GetByIdAsync(id, cancellationToken);

            return _mapper.Map<Permission>(permissionEntity);
        }
    }

}
