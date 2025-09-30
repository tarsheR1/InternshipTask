using Microsoft.AspNetCore.Mvc;
using System.Security;
using AutoMapper;
using UserManagementService.BusinessLogicLayer.Commands;
using UserManagementService.BusinessLogicLayer.Interfaces.Auth;
using UserManagementService.PresentationLayer.DTO.Response;
using UserManagementService.PresentationLayer.DTO.Request;

namespace UserManagementService.PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("users")]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] RegisterUserRequestDto request,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = _mapper.Map<UserRegistrationCommand>(request);
                var authResult = await _authService.RegisterAsync(command, cancellationToken);

                var response = new AuthResponseDto(
                    AccessToken: authResult.AccessToken,
                    RefreshToken: authResult.RefreshToken,
                    Expires: (int)authResult.AccessTokenExpiry.Subtract(DateTime.UtcNow).TotalSeconds,
                    TokenType: "Bearer"
                );

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("tokens")]
        public async Task<IActionResult> LoginAsync(
            [FromBody] LoginRequestDto request,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = _mapper.Map<UserLoginCommand>(request);
                var authResult = await _authService.LoginAsync(command, cancellationToken);

                var response = new AuthResponseDto(
                    AccessToken: authResult.AccessToken,
                    RefreshToken: authResult.RefreshToken,
                    Expires: (int)authResult.AccessTokenExpiry.Subtract(DateTime.UtcNow).TotalSeconds,
                    TokenType: "Bearer"
                );

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
        }

        [HttpPut("tokens")]
        public async Task<IActionResult> RefreshTokenAsync(
            [FromBody] RefreshTokenRequestDto request,
            Guid userId,
            CancellationToken cancellationToken)
        {
            try
            {
                var authResult = await _authService.RefreshTokenAsync(request.RefreshToken, userId, cancellationToken);

                var response = new AuthResponseDto(
                    AccessToken: authResult.AccessToken,
                    RefreshToken: authResult.RefreshToken,
                    Expires: (int)authResult.AccessTokenExpiry.Subtract(DateTime.UtcNow).TotalSeconds,
                    TokenType: "Bearer"
                );

                return Ok(response);
            }
            catch (SecurityException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
        }

        [HttpDelete("tokens")]
        public async Task<IActionResult> RevokeTokenAsync(
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
}