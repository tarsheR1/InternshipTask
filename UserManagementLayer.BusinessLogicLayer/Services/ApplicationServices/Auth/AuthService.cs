using AutoMapper;
using UserManagementService.BusinessLogicLayer.Interfaces.Auth;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.BusinessLogicLayer.Exceptions.Auth;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.BusinessLogicLayer.Models.Queries;
using UserManagementService.DataAccessLayer.Interfaces;
using UserManagementService.DataAccessLayer.Entities.Users;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request.Auth;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IMapper _mapper;
        private readonly string defaultRoleForUser = "User";

        public AuthService(
            IUnitOfWork unitOfWork,
            IJwtTokenGenerator tokenGenerator,
            IPasswordHasher passwordHasher,
            IRefreshTokenService refreshTokenService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
            _passwordHasher = passwordHasher;
            _refreshTokenService = refreshTokenService;
            _mapper = mapper;
        }

        public async Task<AuthResult> RegisterAsync(RegisterUserRequestDto request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var existingUser = await _unitOfWork.Users.GetByEmailAsync(request.Email, cancellationToken);
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
                    Phone = request.Phone,
                    IsActive = false
                };


                var userEntity = _mapper.Map<UserEntity>(user);
                await _unitOfWork.Users.AddAsync(userEntity, cancellationToken);

                var defaultRole = await _unitOfWork.Roles.GetByNameAsync(defaultRoleForUser, cancellationToken);
                userEntity.Roles.Add(defaultRole);

                var accessToken = _tokenGenerator.GenerateToken(user);
                var refreshToken = await _refreshTokenService.GenerateAndSaveRefreshTokenAsync(user.Id, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return new AuthResult(accessToken, refreshToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<AuthResult> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken)
        {
            var userEntity = await _unitOfWork.Users.GetByEmailAsync(request.Email, cancellationToken);

            bool isLoginValid = (userEntity == null || !_passwordHasher.Verify(request.Password, userEntity.PasswordHash));

            if (isLoginValid)
            {
                throw new InvalidCredentialsException();
            }

            var user = _mapper.Map<User>(userEntity);

            var accessToken = _tokenGenerator.GenerateToken(user);
             
            var refreshToken = await _refreshTokenService.GenerateAndSaveRefreshTokenAsync(user.Id, cancellationToken);

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

            var userEntity = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
            if (userEntity == null)
                throw new UserNotFoundException(userId.ToString());

            await _refreshTokenService.RevokeRefreshTokenAsync(refreshToken, cancellationToken);

            var user = _mapper.Map<User>(userEntity);
            var newAccessToken = _tokenGenerator.GenerateToken(user);
            var newRefreshToken = await _refreshTokenService.GenerateAndSaveRefreshTokenAsync(user.Id, cancellationToken);

            return new AuthResult(newAccessToken, newRefreshToken);
        }

        public async Task RevokeTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            await _refreshTokenService.RevokeRefreshTokenAsync(refreshToken, cancellationToken);
        }
    }
}