using AutoMapper;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users; 
using UserManagementService.DataAccessLayer.Entities.Relations;
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
                    throw new NotFoundException(userId.ToString());
                }

                await _roleService.GetRoleByIdAsync(roleId, cancellationToken);

                var existingAssignment = await _unitOfWork.UserRoles.GetAsync(userId, roleId, cancellationToken);
                if (existingAssignment != null)
                {
                    throw new AlreadyExistsException(roleId.ToString());
                }

                var userRole = new UserRole
                {
                    UserId = userId,
                    RoleId = roleId,
                };

                var userRoleEntity = _mapper.Map<UserRoleEntity>(userRole);

                await _unitOfWork.UserRoles.AddAsync(userRoleEntity, cancellationToken);
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
                    throw new NotFoundException(roleId.ToString());
                }

                await _unitOfWork.UserRoles.DeleteAsync(userRole, cancellationToken);
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
                throw new NotFoundException(userId.ToString());
            }

            var roles = await _unitOfWork.UserRoles.GetRolesForUserAsync(userId, cancellationToken);
            return _mapper.Map<List<Role>>(roles);
        }
    }

}
