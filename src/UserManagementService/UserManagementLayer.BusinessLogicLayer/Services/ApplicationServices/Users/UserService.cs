using UserManagementService.DataAccessLayer.Interfaces.Repositories;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using AutoMapper;
using UserManagementService.BusinessLogicLayer.Commands;
using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Entities;

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


        public async Task<User> CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
        {

            var user = new User(
                email: command.Email,
                passwordHash: command.PasswordHash,
                firstName: command.FirstName,
                lastName: command.LastName,
                middleName: command.MiddleName,
                phone: command.Phone
            );

            var userEntity = _mapper.Map<UserEntity>(user);

            await _userRepository.AddAsync(userEntity, cancellationToken);

            return user;
        }

        public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository.GetByIdAsync(userId, cancellationToken);
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
                throw new KeyNotFoundException("User not found.");
            }

            user.Email = updateRequest.Email;
            user.FirstName = updateRequest.FirstName;
            user.LastName = updateRequest.LastName;
            user.MiddleName = updateRequest.MiddleName;
            user.Phone = updateRequest.Phone;

            await _userRepository.UpdateAsync(user, cancellationToken);
        }

        public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            await _userRepository.DeleteAsync(user, cancellationToken);
        }

        public async Task<bool> IsEmailAvailableAsync(string email, CancellationToken cancellationToken = default)
        {
            return !await _userRepository.ExistsByEmailAsync(email, cancellationToken);
        }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<User>(await _userRepository.GetByEmailAsync(email, cancellationToken));
        }
    }
}
