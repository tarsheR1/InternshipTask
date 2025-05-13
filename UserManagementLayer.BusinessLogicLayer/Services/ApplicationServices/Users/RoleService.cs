using AutoMapper;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request.Roles;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.DataAccessLayer.Entities.Role;
using UserManagementService.DataAccessLayer.Interfaces;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Users
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPermissionService _permissionService;

        public RoleService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IPermissionService permissionService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _permissionService = permissionService;
        }

        #region Get Methods

        public async Task<Role> GetRoleByIdAsync(int roleId, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(roleId, cancellationToken);
            if (role == null)
            {
                throw new NotFoundException($"Роль с Id {roleId} не найдена");
            }

            return _mapper.Map<Role>(role);
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork.Roles.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<Role>>(roles);
        }

        public async Task CreateRoleAsync(RoleCreateRequestDto command, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var existingRole = await _unitOfWork.Roles.GetByNameAsync(command.Name, cancellationToken);
                if (existingRole != null)
                {
                    throw new AlreadyExistsException(command.Name);
                }

                var role = new RoleEntity
                {
                    Name = command.Name,
                };

                await _unitOfWork.Roles.AddAsync(role, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<Role> UpdateRoleAsync(int roleId, RoleUpdateRequestDto command, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var role = await _unitOfWork.Roles.GetByIdAsync(roleId, cancellationToken);
                if (role == null)
                {
                    throw new NotFoundException(roleId.ToString());
                }

                if (!string.Equals(role.Name, command.Name, StringComparison.OrdinalIgnoreCase))
                {
                    var existingRole = await _unitOfWork.Roles.GetByNameAsync(command.Name, cancellationToken);
                    if (existingRole != null)
                    {
                        throw new AlreadyExistsException(command.Name);
                    }
                }

                role.Name = command.Name;

                await _unitOfWork.Roles.UpdateAsync(role, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return _mapper.Map<Role>(role);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task DeleteRoleAsync(int roleId, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var role = await _unitOfWork.Roles.GetByIdAsync(roleId, cancellationToken);
                if (role == null)
                {
                    throw new NotFoundException($"Роль с Id {roleId} не найдена");
                }

                await _unitOfWork.Roles.DeleteAsync(role, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        #endregion

        #region Role Query Operations

        

        #endregion

        #region Role-Permission Management

        public async Task AssignPermissionToRoleAsync(int roleId, int permissionId, CancellationToken cancellationToken)
        {
            var permission = await _permissionService.GetPermissionByIdAsync(permissionId, cancellationToken);
            if (permission == null)
            {
                throw new NotFoundException(permissionId.ToString());
            }

            var roleEntity = await _unitOfWork.Roles.GetByIdAsync(roleId, cancellationToken);
            if (roleEntity == null)
            {
                throw new NotFoundException(roleId.ToString());
            }

            var role = _mapper.Map<Role>(roleEntity);

            if (role.Permissions.Contains(permission))
            {
                throw new AlreadyExistsException(permissionId.ToString());
            }

            role.Permissions.Add(permission);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task RemovePermissionFromRoleAsync(int roleId, int permissionId, CancellationToken cancellationToken)
        {
            var permission = await _permissionService.GetPermissionByIdAsync(permissionId, cancellationToken);
            if (permission == null)
            {
                throw new NotFoundException(permissionId.ToString());
            }

            var roleEntity = await _unitOfWork.Roles.GetByIdAsync(roleId, cancellationToken);
            if (roleEntity == null)
            {
                throw new NotFoundException(roleId.ToString());
            }

            var role = _mapper.Map<Role>(roleEntity);

            if (!role.Permissions.Contains(permission))
            {
                throw new NotFoundException(permissionId.ToString());
            }

            role.Permissions.Remove(permission);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Permission>> GetRolePermissionsAsync(int roleId, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(roleId, cancellationToken);
            if (role == null)
            {
                throw new NotFoundException(roleId.ToString());
            }

            return _mapper.Map<List<Permission>>(role.Permissions);
        }

        #endregion
    }
}