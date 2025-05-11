using AutoMapper;
using System.Data;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.DataAccessLayer.Entities.Role;
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

                var role = await _roleService.GetRoleByIdAsync(roleId, cancellationToken);
                if (role == null)
                {
                    throw new NotFoundException(userId.ToString());
                }
                
                var roleEntity = _mapper.Map<RoleEntity>(role);
                if (user.Roles.Contains(roleEntity))
                {
                    throw new AlreadyExistsException(roleEntity.Id.ToString());
                }
                user.Roles.Add(roleEntity);

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
                var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
                if (user == null)
                {
                    throw new NotFoundException(userId.ToString());
                }

                var role = await _roleService.GetRoleByIdAsync(roleId, cancellationToken);
                if (role == null)
                {
                    throw new NotFoundException(userId.ToString());
                }

                var roleEntity = _mapper.Map<RoleEntity>(role);
                if (!user.Roles.Contains(roleEntity))
                {
                    throw new NotFoundException(roleEntity.Id.ToString());
                }
                user.Roles.Remove(roleEntity);

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
            var userEntity = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
            if (userEntity == null)
            {
                throw new NotFoundException(userId.ToString());
            }

            var roles = _mapper.Map<List<Role>>(userEntity.Roles);

            return roles;
        }
    }

}
