using AutoMapper;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.DataAccessLayer.Entities;
using UserManagementService.DataAccessLayer.Interfaces;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Users
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public UserRoleService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IRoleService roleService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task AssignRoleToUserAsync(Guid userId, int roleId, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
                if (user == null)
                {
                    throw new NotFoundException($"User with ID {userId} not found");
                }

                await _roleService.GetRoleByIdAsync(roleId, cancellationToken);

                var existingAssignment = await _unitOfWork.UserRoles.GetAsync(userId, roleId, cancellationToken);
                if (existingAssignment != null)
                {
                    throw new AlreadyExistsException($"User already has this role");
                }

                var userRole = new UserRoleEntity
                {
                    UserId = userId,
                    RoleId = roleId,
                };

                await _unitOfWork.UserRoles.AddRoleToUserAsync(userRole, cancellationToken);
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

                await _unitOfWork.UserRoles.RemoveRoleAssign(userRole, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<List<Role>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {userId} not found");
            }

            var roles = await _unitOfWork.UserRoles.GetRolesForUserAsync(userId, cancellationToken);
            return _mapper.Map<List<Role>>(roles);
        }
    }

}
