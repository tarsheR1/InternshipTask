using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.BusinessLogicLayer.Models.Pagination;
using UserManagementService.DataAccessLayer.Interfaces;
using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AutoMapper.IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, AutoMapper.IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<User>> GetUsersPaginatedAsync(
            PaginationParameters paginationParameters,
            CancellationToken cancellationToken)
        {
            int pageNumber = paginationParameters.PageNumber;
            int pageSize = paginationParameters.PageSize;

            (List<UserEntity> usersEntity, int totalCount) = await _unitOfWork.Users.GetPagedUsersAsync(
                pageNumber,
                pageSize,
                cancellationToken);

            List<User> users= _mapper.Map<List<User>>(usersEntity);

            return new PagedResponse<User>(
                users,
                paginationParameters.PageNumber,
                paginationParameters.PageSize,
                totalCount);
        }

        public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var userEntity = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);

            if (userEntity == null)
            {
                throw new UserNotFoundException(userId.ToString());
            }

            User user = _mapper.Map<User>(userEntity);
            return user;
        }

        public async Task UpdateUserAsync(
            Guid userId,
            UserUpdateCommand updateRequest,
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
        }

        public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException(userId.ToString());
            }

            await _unitOfWork.Users.DeleteAsync(user, cancellationToken);
        }
    }
}