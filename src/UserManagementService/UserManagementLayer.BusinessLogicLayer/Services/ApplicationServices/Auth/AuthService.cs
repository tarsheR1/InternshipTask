using AutoMapper;
using Microsoft.Extensions.Options;
using System.Security;
using UserManagementService.BusinessLogicLayer.Commands;
using UserManagementService.BusinessLogicLayer.Interfaces.Auth;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.BusinessLogicLayer.Queries;
using UserManagementService.BusinessLogicLayer.Settings;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            IUserService userService,
            IJwtTokenGenerator tokenGenerator,
            IPasswordHasher passwordHasher,
            IRefreshTokenService refreshTokenService,
            IOptions<JwtSettings> jwtSettings,
            IMapper mapper)
        {
            _userService = userService;
            _tokenGenerator = tokenGenerator;
            _passwordHasher = passwordHasher;
            _refreshTokenService = refreshTokenService;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }


        public async Task<AuthResult> RegisterAsync(UserRegistrationCommand registrationCommand, CancellationToken cancellationToken)
        {
            var emailAvailable = await _userService.IsEmailAvailableAsync(registrationCommand.Email, cancellationToken);

            if (!emailAvailable)
            {
                throw new ArgumentException("User with this email already exists");
            }

            var passwordHash = _passwordHasher.HashPassword(registrationCommand.Password);

            var createUserCommand = new CreateUserCommand(
               email: registrationCommand.Email,
               passwordHash: passwordHash,  
               firstName: registrationCommand.FirstName,
               lastName: registrationCommand.LastName,
               middleName: registrationCommand.MiddleName,
               phone: registrationCommand.Phone
            );

            var user = await _userService.CreateUserAsync(createUserCommand, cancellationToken);

            var accessToken = _tokenGenerator.GenerateToken(user);
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.Id, cancellationToken);

            return new AuthResult(
                accessToken, 
                refreshToken,
                DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes));
        }


        public async Task<AuthResult> LoginAsync(UserLoginCommand request, CancellationToken cancellation)
        {
            var userEntity = await _userService.GetUserByEmailAsync(request.Email, cancellation);
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
            CancellationToken cancellation)
        {
            var storedToken = await _refreshTokenService.GetRefreshTokenAsync(refreshToken, cancellation);
            if (!await _refreshTokenService.ValidateRefreshTokenAsync(storedToken, cancellation))
                throw new SecurityException("Invalid refresh token");

            var user = await _userService.GetUserByIdAsync(storedToken.UserId);

            await _refreshTokenService.RevokeRefreshTokenAsync(refreshToken, cancellation);

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
