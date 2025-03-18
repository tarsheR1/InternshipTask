using UserManagementService.Application.Interfaces;
using UserManagementService.Core.Models;

namespace UserManagementService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGuidGenerator _guidGenerator;

        public UserService(IUserRepository userRepository, IGuidGenerator guidGenerator)
        {
            _userRepository = userRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<User> CreateUserAsync(string email, string passwordHash, string firstName, string lastName, string middleName, string phone)
        {
            var user = new User(_guidGenerator.NewCombGuid())
            {
                Email = email,
                PasswordHash = passwordHash,
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                Phone = phone,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task UpdateUserAsync(Guid userId, string email, string firstName, string lastName, string middleName, string phone)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            user.Email = email;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.MiddleName = middleName;
            user.Phone = phone;

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            await _userRepository.DeleteAsync(user);
        }

    }
}
