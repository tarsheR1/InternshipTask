using Microsoft.AspNetCore.Mvc;
using UserManagementService.PresentationLayer.DTO.Request;
using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Interfaces.Auth;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;
using UserManagementService.PresentationLayer.DTO.Response;

namespace UserManagementService.PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("registrations")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserRequestDto request,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UserRegistrationCommand>(request);

            var authResult = await _authService.RegisterAsync(command, cancellationToken);

            var response = new AuthResponseDto(
                AccessToken: authResult.AccessToken,
                RefreshToken: authResult.RefreshToken);

            return Ok(response);
        }

        [HttpPost("sessions")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestDto request,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UserLoginCommand>(request);

            var authResult = await _authService.LoginAsync(command, cancellationToken);

            var response = new AuthResponseDto(
                AccessToken: authResult.AccessToken,
                RefreshToken: authResult.RefreshToken);

            return Ok(response);  
        }

        [HttpPost("tokens/refresh")]
        public async Task<IActionResult> RefreshToken(
            [FromBody] RefreshTokenRequestDto request,
            CancellationToken cancellationToken)
        {
            var authResult = await _authService.RefreshTokenAsync(request.RefreshToken, request.UserId, cancellationToken);

            var response = new AuthResponseDto(
                AccessToken: authResult.AccessToken,
                RefreshToken: authResult.RefreshToken);

            return Ok(response);
        }

        [HttpPost("tokens/revoked")]
        public async Task<IActionResult> RevokeToken(
            [FromBody] RefreshTokenRequestDto request,
            CancellationToken cancellationToken)
        {
            await _authService.RevokeTokenAsync(request.RefreshToken, cancellationToken);

            return NoContent();
        }
    }
}