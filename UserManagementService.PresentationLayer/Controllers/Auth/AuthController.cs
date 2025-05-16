using Microsoft.AspNetCore.Mvc;
using Serilog;
using UserManagementService.BusinessLogicLayer.Interfaces.Auth;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request.Auth;
using UserManagementService.BusinessLogicLayer.Models.DTO.Response;

namespace UserManagementService.PresentationLayer.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IAuthService authService,
            ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserRequestDto request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Register attempt for email: {Email}", request.Email);

            try
            {
                var result = await _authService.RegisterAsync(request, cancellationToken);
                _logger.LogInformation("User {Email} registered successfully", request.Email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user {Email}", request.Email);
                throw; // Global error handler will catch this
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestDto request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Login attempt for email: {Email}", request.Email);

            try
            {
                var authResult = await _authService.LoginAsync(request, cancellationToken);

                if (authResult == null)
                {
                    _logger.LogWarning("Invalid login attempt for email: {Email}", request.Email);
                    return Unauthorized();
                }

                _logger.LogInformation("User {Email} logged in successfully", request.Email);
                return Ok(authResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for email: {Email}", request.Email);
                throw;
            }
        }

        [HttpPost("refresh-tokens")]
        public async Task<IActionResult> RefreshToken(
            [FromBody] RefreshTokenRequestDto request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Refresh token request for user ID: {UserId}", request.UserId);

            try
            {
                var authResult = await _authService.RefreshTokenAsync(
                    request.RefreshToken,
                    request.UserId,
                    cancellationToken);

                if (authResult == null)
                {
                    _logger.LogWarning("Invalid refresh token for user ID: {UserId}", request.UserId);
                    return Unauthorized();
                }

                _logger.LogInformation("Tokens refreshed successfully for user ID: {UserId}", request.UserId);
                return Ok(authResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing tokens for user ID: {UserId}", request.UserId);
                throw;
            }
        }

        [HttpPost("refresh-tokens/revoked")]
        public async Task<IActionResult> RevokeToken(
            [FromBody] RefreshTokenRequestDto request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Revoke token request for user ID: {UserId}", request.UserId);

            try
            {
                await _authService.RevokeTokenAsync(request.RefreshToken, cancellationToken);
                _logger.LogInformation("Token revoked successfully for user ID: {UserId}", request.UserId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error revoking token for user ID: {UserId}", request.UserId);
                throw;
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(
            [FromQuery] Guid userId,
            [FromQuery] string token,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Email confirmation request for user ID: {UserId}", userId);

            try
            {
                var result = await _authService.ConfirmEmailAsync(userId, token, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Email confirmed successfully for user ID: {UserId}", userId);
                    return Ok();
                }

                _logger.LogWarning("Email confirmation failed for user ID: {UserId}", userId);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error confirming email for user ID: {UserId}", userId);
                throw;
            }
        }
    }
}