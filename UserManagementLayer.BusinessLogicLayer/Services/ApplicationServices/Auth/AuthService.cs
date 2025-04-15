using UserManagementService.BusinessLogicLayer.Interfaces.Auth;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;
using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.BusinessLogicLayer.Exceptions.Auth;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.DataAccessLayer.Interfaces;
using UserManagementService.DataAccessLayer.Entities;
using UserManagementService.BusinessLogicLayer.Models.Queries;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly AutoMapper.IMapper _mapper;

        public AuthService(
            IUnitOfWork unitOfWork,
            IJwtTokenGenerator tokenGenerator,
            IPasswordHasher passwordHasher,
            IRefreshTokenService refreshTokenService,
            AutoMapper.IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
            _passwordHasher = passwordHasher;
            _refreshTokenService = refreshTokenService;
            _mapper = mapper;
        }

        public async Task<AuthResult> RegisterAsync(UserRegistrationCommand request, CancellationToken cancellationToken)
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
                    Phone = request.Phone
                };

                var defaultRole = await _unitOfWork.Roles.GetByNameAsync("User", cancellationToken);
                await _unitOfWork.Roles.AddAsync(defaultRole, cancellationToken);
                

                var userEntity = _mapper.Map<UserEntity>(user);
                await _unitOfWork.Users.AddAsync(userEntity, cancellationToken);

                var assignedRole = new UserRoleEntity
                {
                    UserId = user.Id,
                    RoleId = defaultRole.Id,
                };
                await _unitOfWork.UserRoles.AddAsync(assignedRole, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                var accessToken = _tokenGenerator.GenerateToken(user);
                var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.Id, cancellationToken);

                return new AuthResult(accessToken, refreshToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<AuthResult> LoginAsync(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var userEntity = await _unitOfWork.Users.GetByEmailAsync(request.Email, cancellationToken);

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

            var userEntity = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
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