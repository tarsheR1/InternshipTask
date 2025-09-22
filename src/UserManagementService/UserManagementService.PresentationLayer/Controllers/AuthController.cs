using Microsoft.AspNetCore.Mvc;
using UserManagementService.PresentationLayer.DTO.Request;
using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Interfaces.Auth;
using System.Security;
using AutoMapper;

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

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserRequestDto request,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = _mapper.Map<UserRegistrationCommand>(request);
                var authResult = await _authService.RegisterAsync(command, cancellationToken);

                var response = new AuthResponseDto
                {
                    AccessToken = authResult.AccessToken,
                    RefreshToken = authResult.RefreshToken,
                    ExpiresIn = (int)authResult.AccessTokenExpiry.Subtract(DateTime.UtcNow).TotalSeconds
                };

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestDto request,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = _mapper.Map<UserLoginCommand>(request);
                var authResult = await _authService.LoginAsync(command, cancellationToken);

                var response = new AuthResponseDto
                {
                    AccessToken = authResult.AccessToken,
                    RefreshToken = authResult.RefreshToken,
                    ExpiresIn = (int)authResult.AccessTokenExpiry.Subtract(DateTime.UtcNow).TotalSeconds
                };

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(
            [FromBody] RefreshTokenRequestDto request,
            Guid userId,
            CancellationToken cancellationToken)
        {
            try
            {
                var authResult = await _authService.RefreshTokenAsync(request.RefreshToken, userId, cancellationToken);

                var response = new AuthResponseDto
                {
                    AccessToken = authResult.AccessToken,
                    RefreshToken = authResult.RefreshToken,
                    ExpiresIn = (int)authResult.AccessTokenExpiry.Subtract(DateTime.UtcNow).TotalSeconds
                };

                return Ok(response);
            }
            catch (SecurityException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(
            [FromBody] RefreshTokenRequestDto request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _authService.RevokeTokenAsync(request.RefreshToken, cancellationToken);
                return NoContent();
            }
            catch (SecurityException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

    public class RefreshTokenRequestDto
    {
        public string RefreshToken { get; set; }
    }

    public class AuthResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; } = "Bearer";
    }
}