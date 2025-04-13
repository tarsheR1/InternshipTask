using UserManagementService.BusinessLogicLayer.Interfaces.Auth;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;
using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.BusinessLogicLayer.Exceptions.Auth;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.DataAccessLayer.Interfaces.Repositories;
using UserManagementService.BusinessLogicLayer.Models.Queries;
using AutoMapper;
using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly AutoMapper.IMapper _mapper;

        public AuthService(
            IUserRepository userRepository,
            IJwtTokenGenerator tokenGenerator,
            IPasswordHasher passwordHasher,
            IRefreshTokenService refreshTokenService,
            AutoMapper.IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _passwordHasher = passwordHasher;
            _refreshTokenService = refreshTokenService;
            _mapper = mapper;
        }

        public async Task<AuthResult> RegisterAsync(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingUser != null)
                throw new EmailAlreadyExistsException(request.Email);

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

            // TODO: ADD BASE ROLE FOR USER

            var userEntity = _mapper.Map<UserEntity>(user);
            await _userRepository.AddAsync(userEntity, cancellationToken);

            var accessToken = _tokenGenerator.GenerateToken(user);
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.Id, cancellationToken);

            return new AuthResult(accessToken, refreshToken);
        }

        public async Task<AuthResult> LoginAsync(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (userEntity == null)
                throw new InvalidCredentialsException();

            if (!_passwordHasher.Verify(request.Password, userEntity.PasswordHash))
                throw new InvalidCredentialsException();

            var user = _mapper.Map<User>(userEntity);

            var accessToken = _tokenGenerator.GenerateToken(user);
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.Id, cancellationToken);

            return new AuthResult(accessToken, refreshToken);
        }

        public async Task<AuthResult> RefreshTokenAsync(
            string refreshToken,
            Guid userId,
            CancellationToken cancellationToken)
        {
            bool isTokenValid = await _refreshTokenService.ValidateRefreshTokenAsync(refreshToken, cancellationToken);
            if (!isTokenValid)
                throw new InvalidRefreshTokenException();

            await _refreshTokenService.RevokeRefreshTokenAsync(refreshToken, cancellationToken);

            var userEntity = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if (userEntity == null)
                throw new UserNotFoundException(userId.ToString());

            var user = _mapper.Map<User>(userEntity);
            var newAccessToken = _tokenGenerator.GenerateToken(user);
            var newRefreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.Id, cancellationToken);

            return new AuthResult(newAccessToken, newRefreshToken);
        }

        public async Task RevokeTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            await _refreshTokenService.RevokeRefreshTokenAsync(refreshToken, cancellationToken);
        }
    }
}