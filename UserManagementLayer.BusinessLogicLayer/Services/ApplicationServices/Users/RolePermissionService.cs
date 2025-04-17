using AutoMapper;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.DataAccessLayer.Interfaces;
using UserManagementService.DataAccessLayer.Entities;

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

        public async Task AssignPermissionToRoleAsync(int roleId, int permissionId, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await _roleService.GetRoleByIdAsync(roleId, cancellationToken);

                var permissions = await _permissionService.GetAllPermissionsAsync(cancellationToken);
                if (!permissions.Any(p => p.Id == permissionId))
                {
                    throw new NotFoundException($"Разрешение с ID {permissionId} не найдено");
                }

                var existingAssignment = await _unitOfWork.RolePermission.GetAsync(roleId, permissionId, cancellationToken);
                if (existingAssignment != null)
                {
                    throw new AlreadyExistsException($"Разрешение уже назначено для роли");
                }

                var rolePermission = new RolePermissionEntity
                {
                    RoleId = roleId,
                    PermissionId = permissionId,
                };

                await _unitOfWork.RolePermission.AddAsync(rolePermission, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task RemovePermissionFromRoleAsync(int roleId, int permissionId, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var rolePermission = await _unitOfWork.RolePermission.GetAsync(roleId, permissionId, cancellationToken);
                if (rolePermission == null)
                {
                    throw new NotFoundException($"Разрешение не назначено к роли");
                } 

                await _unitOfWork.RolePermission.DeleteAsync(roleId, permissionId, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<List<Permission>> GetRolePermissionsAsync(int roleId, CancellationToken cancellationToken)
        {
            await _roleService.GetRoleByIdAsync(roleId, cancellationToken);

            var permissions = await _unitOfWork.RolePermission.GetPermissionsForRoleAsync(roleId, cancellationToken);
            return _mapper.Map<List<Permission>>(permissions);
        }
    }

}
