using AutoMapper;
using Shared.Pagination;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.BusinessLogicLayer.Models.Queries;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.DataAccessLayer.Interfaces;
using UserManagementService.DataAccessLayer.Specifications.Users;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<User>> GetUsersPaginatedAsync(
            PaginationParameters paginationParameters,
            UserFilter filter = null,
            SortOptions sort = null,
            CancellationToken cancellationToken = default)
        {
            var spec = new UserSpecification(filter);

            if (sort != null)
            {
                spec.ApplyOrdering(sort.Field, sort.IsDescending);
            }

            var (usersEntity, totalCount) = await _unitOfWork.Users.GetAllBySpecAsync(
                spec,
                paginationParameters,
                cancellationToken);

            var users = _mapper.Map<List<User>>(usersEntity);

            return new PagedResponse<User>(
                users,
                paginationParameters.PageNumber,
                paginationParameters.PageSize,
                totalCount);
        }

        public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var spec = new UserByIdSpecification(userId);
            var userEntity = await _unitOfWork.Users.GetBySpecAsync(spec, cancellationToken);

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

        public async Task<List<string>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Users.GetUserRolesAsync(userId, cancellationToken);
        }

        public async Task UpdateUserAsync(
            Guid userId,
            UserUpdateCommand updateRequest,
            CancellationToken cancellationToken)
        {
            var spec = new UserByIdSpecification(userId);
            var user = await _unitOfWork.Users.GetBySpecAsync(spec, cancellationToken);

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
        }

        public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var spec = new UserByIdSpecification(userId);
            var user = await _unitOfWork.Users.GetBySpecAsync(spec, cancellationToken);

            if (user == null)
            {
                throw new UserNotFoundException(userId.ToString());
            }

            await _unitOfWork.Users.DeleteAsync(user, cancellationToken);
        }
    }
}