using UserManagementService.BusinessLogicLayer.Extensions;
using UserManagementService.BusinessLogicLayer.Models;
using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Services.Interfaces;
using UserManagementService.DataAccessLayer.Interfaces;
using UserManagementService.DataAccessLayer.Entities;


namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IPasswordHasher _passwodHasher;
        private readonly IMapper _mapper;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly TimeSpan _accessTokenLifetime = TimeSpan.FromMinutes(15);

        public AuthService(
            IRefreshTokenService refresh,
            IUserRepository userRepository,
            IJwtTokenGenerator tokenGenerator,
            IPasswordHasher passwordHasher,
            IMapper mapper)
        {
            _refreshTokenService = refresh;
            _userRepository = userRepository;
            _passwodHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
        }
        
        public async Task<string> RegisterAsync(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingUser != null)
                throw new ArgumentException("User with this email already exists");

            var passwordHash = _passwodHasher.HashPassword(request.Password);
            User user = new User()
            {
                Id = Guid.NewGuid().NewCombGuid(),
                Email = request.Email,
                PasswordHash = passwordHash,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Phone = request.Phone
            };

            var userEntity = _mapper.Map<UserEntity>(user);
            await _userRepository.AddAsync(userEntity, cancellationToken);

            return _tokenGenerator.GenerateToken(user);
        }

        public async Task<string> LoginAsync(UserLoginCommand loginRequest, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository.GetByEmailAsync(loginRequest.Email, cancellationToken);
            if (userEntity == null)
                throw new ArgumentException("User not found");

            if (_passwodHasher.Verify(loginRequest.Password, userEntity.PasswordHash))
                throw new ArgumentException("Invalid password");

            var user = _mapper.Map<User>(userEntity);
            return _tokenGenerator.GenerateToken(user);
        }
    }
}
