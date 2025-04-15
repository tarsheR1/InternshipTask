using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.DataAccessLayer.Entities;
using UserManagementService.DataAccessLayer.Interfaces;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Users
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AutoMapper.IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, AutoMapper.IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateRoleAsync(RoleCreateCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var existingRole = await _unitOfWork.Roles.GetByNameAsync(command.Name, cancellationToken);
                if (existingRole != null)
                {
                    throw new AlreadyExistsException($"Роль '{command.Name}' уже существует");
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

        public async Task<Role> UpdateRoleAsync(int roleId, RoleUpdateCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var role = await _unitOfWork.Roles.GetByIdAsync(roleId, cancellationToken);
                if (role == null)
                {
                    throw new NotFoundException($"Role with ID {roleId} not found");
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

                var userRoles = await _unitOfWork.UserRoles.GetUsersForRoleAsync(roleId, cancellationToken);
                if (userRoles.Any())
                {
                    throw new ConflictException($"Cannot delete role '{role.Name}' as it is assigned to {userRoles.Count()} users");
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
    }
}
