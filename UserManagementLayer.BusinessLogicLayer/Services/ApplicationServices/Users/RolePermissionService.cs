using AutoMapper;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.DataAccessLayer.Interfaces;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Users
{
    public class RolePermissionAssignmentService : IRolePermissionAssignmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;

        public RolePermissionAssignmentService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IRoleService roleService,
            IPermissionService permissionService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleService = roleService;
            _permissionService = permissionService;
        }

        

        public async Task RemovePermissionFromRoleAsync(int roleId, int permissionId, CancellationToken cancellationToken)
        {
            var permission = await _permissionService.GetPermissionByIdAsync(permissionId, cancellationToken);
            if (permission == null)
            {
                throw new NotFoundException(permissionId.ToString());
            }

            var role = await _roleService.GetRoleByIdAsync(roleId, cancellationToken);
            if (role == null)
            {
                throw new NotFoundException(roleId.ToString());
            }

            role.Permissions.Remove(permission);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Permission>> GetRolePermissionsAsync(int roleId, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(roleId, cancellationToken);
            if(role == null)
            {
                throw new NotFoundException(roleId.ToString());
            }

            var permissions = role.Permissions;

            return _mapper.Map<List<Permission>>(permissions);
        }
        
    }

}
