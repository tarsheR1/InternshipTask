<<<<<<< Updated upstream
﻿using System.Security.Claims;
using System.Text;
using UserManagementService.Application.Interfaces;
=======
﻿using UserManagementService.Application.Extensions;
using UserManagementService.Core.Interfaces;
>>>>>>> Stashed changes
using UserManagementService.Core.Models;

namespace UserManagementService.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _tokenGenerator;
<<<<<<< Updated upstream

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
=======
        private readonly IPasswordHasher _passwodHasher;

        public AuthService(
            IUserRepository userRepository, 
            IPasswordHasher passwordHasher, 
            IJwtTokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _passwodHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
>>>>>>> Stashed changes
        }

        // PEREDELAT!!!!!!
        public async Task<string> RegisterAsync(
            string email, 
            string password, 
            string firstName, 
            string lastName, 
            string middleName, 
            string phone)
        {
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
                throw new ArgumentException("User with this email already exists");

<<<<<<< Updated upstream
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Id = Guid.NewGuid(),
=======
            var passwordHash = _passwodHasher.HashPassword(password);
            User user = new User()
            {
                Id = Guid.NewGuid().NewCombGuid(),
>>>>>>> Stashed changes
                Email = email,
                PasswordHash = passwordHash,
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                Phone = phone
            };

            await _userRepository.AddAsync(user);

<<<<<<< Updated upstream
            return GenerateJwtToken(user);
=======
            return _tokenGenerator.GenerateToken(user);
>>>>>>> Stashed changes
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                throw new ArgumentException("User not found");

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new ArgumentException("Invalid password");

<<<<<<< Updated upstream
            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
                }),
                Expires = DateTime.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes),
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
=======
            return _tokenGenerator.GenerateToken(user);
>>>>>>> Stashed changes
        }
    }
}
