using AutoMapper;
using Shared.Pagination;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.BusinessLogicLayer.Models.Queries;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.DataAccessLayer.Interfaces;
using UserManagementService.DataAccessLayer.Entities.Role;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request.Users;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Users
{
    public class UserService : IUserService, IUserRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IRoleService roleService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleService = roleService;
        }


        public async Task AddUserAsync(CancellationToken cancellationToken)
        {

        }

        #region Get Methods

        public async Task<PagedResponse<User>> GetUsersPaginatedAsync(
            PaginationParameters paginationParameters,
            //UserFilter filter = null,
            SortOptions sort = null,
            CancellationToken cancellationToken = default)
        {
            //    var spec = new UserSpecification(filter);

            //    if (sort != null)
            //    {
            //        spec.ApplyOrdering(sort.Field, sort.IsDescending);
            //    }

            //    var query = _unitOfWork.Users.GetAllBySpecAsync(spec);

            //    var totalCount = await _unitOfWork.Users.CountBySpecAsync(spec);

            //    var usersEntity = await query
            //        .Skip(paginationParameters.PageNumber)
            //        .Take(paginationParameters.PageSize)
            //        .ToListAsync(cancellationToken);

            //    var users = _mapper.Map<List<User>>(usersEntity);

            //    return new PagedResponse<User>(
            //        users,
            //        paginationParameters.PageNumber,
            //        paginationParameters.PageSize,
            //        totalCount);
            //
            return new PagedResponse<User>(new List<User>(), 3, 3,3);

        }

        public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var userEntity = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
            if (userEntity == null)
            {
                throw new UserNotFoundException(userId.ToString());
            }

            return _mapper.Map<User>(userEntity);
        }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var userEntity = await _unitOfWork.Users.GetByEmailAsync(email, cancellationToken);

            if (userEntity == null)
            {
                throw new UserNotFoundException(email);
            }

            return _mapper.Map<User>(userEntity);
        }

        public async Task<List<Role>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken)
        {
            var roleEnities = await _unitOfWork.Users.GetUserRolesAsync(userId, cancellationToken);
            return _mapper.Map<List<Role>>(roleEnities);
        }

        #endregion

        #region Update Methods

        public async Task UpdateUserAsync(
            Guid userId,
            UpdateUserRequestDto updateRequest,
            CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException(userId.ToString());
            }

            user.Email = updateRequest.Email;
            user.FirstName = updateRequest.FirstName;
            user.LastName = updateRequest.LastName;
            user.MiddleName = updateRequest.MiddleName;
            user.Phone = updateRequest.Phone;

            await _unitOfWork.Users.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region Delete Methods

        public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException(userId.ToString());
            }

            await _unitOfWork.Users.DeleteAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region User-Role Management

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

        #endregion
    }
}