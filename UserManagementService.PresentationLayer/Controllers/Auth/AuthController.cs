using Microsoft.AspNetCore.Mvc;
using UserManagementService.BusinessLogicLayer.Interfaces.Auth;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request;
using UserManagementService.BusinessLogicLayer.Models.DTO.Response;

namespace UserManagementService.PresentationLayer.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("users")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserRequestDto request,
            CancellationToken cancellationToken)
        {
            var authResult = await _authService.RegisterAsync(request, cancellationToken);

            return Ok(authResult);
        }

        [HttpPost("auth/token")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestDto request,
            CancellationToken cancellationToken)
        {
            var authResult = await _authService.LoginAsync(request, cancellationToken);

            return Ok(authResult);  
        }

        [HttpPost("tokens/refresh")]
        public async Task<IActionResult> RefreshToken(
            [FromBody] RefreshTokenRequestDto request,
            CancellationToken cancellationToken)
        {
            var authResult = await _authService.RefreshTokenAsync(request.RefreshToken, request.UserId, cancellationToken);

            return Ok(authResult);
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