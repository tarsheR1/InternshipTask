using AutoMapper;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.DataAccessLayer.Entities;
using UserManagementService.DataAccessLayer.Interfaces;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Users
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RolePermissionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Role Management

        public async Task<Role> CreateRoleAsync(RoleCreateCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var existingRole = await _unitOfWork.Roles.GetByNameAsync(command.Name, cancellationToken);
                if (existingRole != null)
                {
                    throw new ArgumentException($"Role with name '{command.Name}' already exists");
                }

                var role = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = command.Name,
                };

                await _unitOfWork.Roles.AddAsync(role, cancellationToken);
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

        public async Task<Role> UpdateRoleAsync(int roleId, RoleUpdateCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var role = await _unitOfWork.Roles.GetByIdAsync(roleId, cancellationToken);
                if (role == null)
                {
                    throw new ArgumentException($"Role with ID {roleId} not found");
                }

                if (!string.Equals(role.Name, command.Name, StringComparison.OrdinalIgnoreCase))
                {
                    var existingRole = await _unitOfWork.Roles.GetByNameAsync(command.Name, cancellationToken);
                    if (existingRole != null)
                    {
                        throw new ArgumentException($"Role with name '{command.Name}' already exists");
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
                    throw new NotFoundException($"Role with ID {roleId} not found");
                }

                var userRoles = await _unitOfWork.UserRoles.GetUsersForRoleAsync(roleId, cancellationToken);
                if (userRoles.Count() > 0)
                {
                    throw new Exception($"Cannot delete role '{role.Name}' as it is assigned to {userRoles.Count()} users");
                }

                var rolePermissions = await _unitOfWork.RolePermission.GetPermissionsForRoleAsync(roleId, cancellationToken);
                foreach (var rp in rolePermissions)
                {
                    _unitOfWork.RolePermission.DeleteAsync(roleId, rp.Id, cancellationToken);
                }

                _unitOfWork.Roles.DeleteAsync(role, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<Role> GetRoleByIdAsync(int roleId, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(roleId, cancellationToken);
            if (role == null)
            {
                throw new NotFoundException($"Role with ID {roleId} not found");
            }

            return _mapper.Map<Role>(role);
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork.Roles.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<Role>>(roles);
        }

        #endregion

        #region Permission Management

        public async Task<IEnumerable<Permission>> GetAllPermissionsAsync(CancellationToken cancellationToken)
        {
            var permissions = await _unitOfWork.Perm.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<Permission>>(permissions);
        }

        #endregion

        #region Role-Permission Assignment

        public async Task AssignPermissionToRoleAsync(int roleId, int permissionId, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var role = await _unitOfWork.Roles.GetByIdAsync(roleId, cancellationToken);
                if (role == null)
                {
                    throw new NotFoundException($"Role with ID {roleId} not found");
                }

                var permission = await _unitOfWork.Permissions.GetByIdAsync(permissionId, cancellationToken);
                if (permission == null)
                {
                    throw new NotFoundException($"Permission with ID {permissionId} not found");
                }

                var existingAssignment = await _unitOfWork.RolePermission.GetAsync(roleId, permissionId, cancellationToken);
                if (existingAssignment != null)
                {
                    throw new Exception($"Permission '{permission.Name}' is already assigned to role '{role.Name}'");
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
                    throw new NotFoundException($"Разрешение не прикреплено к роли");
                }

                _unitOfWork.RolePermission.DeleteAsync(roleId, permissionId, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<IEnumerable<Permission>> GetRolePermissionsAsync(int roleId, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(roleId, cancellationToken);
            if (role == null)
            {
                throw new NotFoundException($"Роль {roleId} не найдена");
            }

            var permissions = await _unitOfWork.  .GetPermissionsByRoleIdAsync(roleId, cancellationToken);
            return _mapper.Map<IEnumerable<Permission>>(permissions);
        }

        #endregion

        #region User-Role Assignment

        public async Task AssignRoleToUserAsync(Guid userId, int roleId, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
                if (user == null)
                {
                    throw new NotFoundException($"Пользователь {userId} не найден");
                }

                var role = await _unitOfWork.Roles.GetByIdAsync(roleId, cancellationToken);
                if (role == null)
                {
                    throw new NotFoundException($"Роль по ID {roleId} не найдена");
                }

                var existingAssignment = await _unitOfWork.UserRoles.GetAsync(userId, roleId, cancellationToken);
                if (existingAssignment != null)
                {
                    throw new ArgumentException($"Роль уже привязана '{role.Name}'");
                }

                var userRole = new UserRoleEntity
                {
                    UserId = userId,
                    RoleId = roleId,
                };

                await _unitOfWork.UserRoles.AddAsync(userRole, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task RemoveRoleFromUserAsync(Guid userId, int roleId, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var userRole = await _unitOfWork.UserRoles.GetAsync(userId, roleId, cancellationToken);
                if (userRole == null)
                {
                    throw new NotFoundException($"User does not have the specified role");
                }

                _unitOfWork.UserRoles.DeleteAsync(userId, roleId, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<IEnumerable<Role>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {userId} not found");
            }

            var roles = await _unitOfWork.UserRoles.GetRolesForUserAsync(userId, cancellationToken);
            return _mapper.Map<IEnumerable<Role>>(roles);
        }

        #endregion
    }

}
