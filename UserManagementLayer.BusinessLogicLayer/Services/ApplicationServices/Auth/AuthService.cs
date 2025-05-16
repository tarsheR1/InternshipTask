using AutoMapper;
using UserManagementService.BusinessLogicLayer.Interfaces.Auth;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.BusinessLogicLayer.Exceptions.Auth;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.BusinessLogicLayer.Models.Queries;
using UserManagementService.DataAccessLayer.Interfaces;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request.Auth;
using UserManagementService.BusinessLogicLayer.Models.DTO.Response;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request.Users;
using Microsoft.AspNetCore.Mvc;
using Hangfire;
using Microsoft.AspNetCore.Http;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobClient _backgroundJob;
        private readonly IUrlHelper _urlHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(
            IUnitOfWork unitOfWork,
            IJwtTokenGenerator tokenGenerator,
            IPasswordHasher passwordHasher,
            IRefreshTokenService refreshTokenService,
            IUserService userService,
            IMapper mapper,
            IBackgroundJobClient backgroundJob,
            IUrlHelper urlHelper,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
            _passwordHasher = passwordHasher;
            _refreshTokenService = refreshTokenService;
            _userService = userService;
            _mapper = mapper;
            _backgroundJob = backgroundJob;
            _urlHelper = urlHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RegisterUserResponseDto> RegisterAsync(RegisterUserRequestDto request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var existingUser = await _userService.GetUserByEmailAsync(request.Email, cancellationToken);
                if (existingUser != null)
                    throw new EmailAlreadyExistsException(request.Email);

                var passwordHash = _passwordHasher.HashPassword(request.Password);
                 
                var createUserRequest = new CreateUserRequestDto
                {
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    FirstName = request.FirstName,
                    MiddleName = request.MiddleName,
                    LastName = request.LastName,
                    Phone = request.Phone
                };

                var user = await _userService.CreateUserAsync(createUserRequest, cancellationToken);

                var token = await GenerateEmailConfirmationTokenAsync(user.Id, cancellationToken);

                var callbackUrl = _urlHelper.Action(
                     "ConfirmEmail",
                     "Auth",
                     new { userId = user.Id, token },
                     protocol: _httpContextAccessor.HttpContext?.Request.Scheme ?? "https");

                _backgroundJob.Enqueue<IEmailService>(x =>
                    x.SendConfirmationEmailAsync(user.Email, user.Id, callbackUrl));

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return new RegisterUserResponseDto
                {
                    Message = "User registered successfully. Please check your email for confirmation.",
                    UserId = user.Id
                };
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<AuthResult> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken)
        {
            var userEntity = await _userService.GetUserByEmailAsync(request.Email, cancellationToken);

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

        public async Task<bool> ConfirmEmailAsync(Guid userId, string token, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(userId, cancellationToken);
            if (user == null) return false;

            if (user.IsActive) return true; 

            if (user.EmailConfirmationToken != token) 
            {
                return false; 
            }

            user.IsActive = true;
            user.EmailConfirmationToken = null;

            return true;
        }   

        public async Task<string> GenerateEmailConfirmationTokenAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(userId, cancellationToken);
            if (user == null) throw new UserNotFoundException(userId.ToString());

            var token = Guid.NewGuid().ToString("N");

            user.EmailConfirmationToken = token;

            UpdateUserRequestDto requestDto = new UpdateUserRequestDto
            {
                EmailActivationToken = token
            };

            await _userService.UpdateUserAsync(userId, requestDto, cancellationToken);

            return token;
        }
    }
}