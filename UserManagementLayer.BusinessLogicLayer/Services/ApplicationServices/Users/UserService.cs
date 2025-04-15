using UserManagementService.DataAccessLayer.Interfaces.Repositories;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.BusinessLogicLayer.Models.Pagination;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<User>> GetUsersPaginatedAsync(
            PaginationParameters paginationParameters,
            CancellationToken cancellationToken)
        {


            return new PagedResponse<User>(
                users,
                paginationParameters.PageNumber,
                paginationParameters.PageSize,
                totalCount);
        }

        public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository.GetByIdAsync(userId, cancellationToken);

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
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException(userId.ToString());
            }

            user.Email = updateRequest.Email;
            user.FirstName = updateRequest.FirstName;
            user.LastName = updateRequest.LastName;
            user.MiddleName = updateRequest.MiddleName;
            user.Phone = updateRequest.Phone;

            await _userRepository.UpdateAsync(user, cancellationToken);
        }

        public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException(userId.ToString());
            }

            await _userRepository.DeleteAsync(user, cancellationToken);
        }
    }
}