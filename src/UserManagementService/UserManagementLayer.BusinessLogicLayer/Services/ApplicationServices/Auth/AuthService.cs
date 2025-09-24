using AutoMapper;
using Microsoft.Extensions.Options;
using System.Security;
using UserManagementService.BusinessLogicLayer.Commands;
using UserManagementService.BusinessLogicLayer.Interfaces.Auth;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.BusinessLogicLayer.Models.Settings;
using UserManagementService.BusinessLogicLayer.Queries;
using UserManagementService.DataAccessLayer.Entities;
using UserManagementService.DataAccessLayer.Interfaces.Repositories;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            IUserRepository userRepository,
            IJwtTokenGenerator tokenGenerator,
            IPasswordHasher passwordHasher,
            IRefreshTokenService refreshTokenService,
            IOptions<JwtSettings> jwtSettings,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _passwordHasher = passwordHasher;
            _refreshTokenService = refreshTokenService;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }


        public async Task<AuthResult> RegisterAsync(UserRegistrationCommand request, CancellationToken cancellation)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellation);
            if (existingUser != null)
                throw new ArgumentException("User with this email already exists");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Phone = request.Phone
            };

            var userEntity = _mapper.Map<UserEntity>(user);
            await _userRepository.AddAsync(userEntity, cancellation);

            var accessToken = _tokenGenerator.GenerateToken(user);
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.Id, cancellation);

            return new AuthResult(
                accessToken, 
                refreshToken,
                DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes));
        }


        public async Task<AuthResult> LoginAsync(UserLoginCommand request, CancellationToken cancellation)
        {
            var userEntity = await _userRepository.GetByEmailAsync(request.Email, cancellation);
            if (userEntity == null || !_passwordHasher.Verify(request.Password, userEntity.PasswordHash))
                throw new ArgumentException("Invalid email or password");

            var user = _mapper.Map<User>(userEntity);
            var accessToken = _tokenGenerator.GenerateToken(user);
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.Id, cancellation);

            return new AuthResult(
                accessToken,
                refreshToken,
                DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes));
        }


        public async Task<AuthResult> RefreshTokenAsync(
            string refreshToken, 
            Guid userId, 
            CancellationToken cancellation)
        {
            if (!await _refreshTokenService.ValidateRefreshTokenAsync(refreshToken, cancellation))
                throw new SecurityException("Invalid refresh token");

            await _refreshTokenService.RevokeRefreshTokenAsync(refreshToken, cancellation);

            var userEntity = await _userRepository.GetByIdAsync(userId, cancellation);
            var user = _mapper.Map<User>(userEntity);

            var newAccessToken = _tokenGenerator.GenerateToken(user);
            var newRefreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.Id, cancellation);

            return new AuthResult(
                newAccessToken,
                newRefreshToken,
                DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes));
        }


        public async Task RevokeTokenAsync(string refreshToken, CancellationToken cancellation)
        {
            await _refreshTokenService.RevokeRefreshTokenAsync(refreshToken, cancellation);
        }
    }
}
