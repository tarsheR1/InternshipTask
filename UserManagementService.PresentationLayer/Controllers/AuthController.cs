using Microsoft.AspNetCore.Mvc;
using UserManagementService.BusinessLogicLayer.Services.Interfaces;
using UserManagementService.PresentationLayer.DTO.Request;
using UserManagementService.BusinessLogicLayer.Models.Commands;

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
            var userRegistrationCommand = _mapper.Map<UserRegistrationCommand>(request);
            var token = await _authService.RegisterAsync(userRegistrationCommand, cancellationToken);

            return Ok(new { Token = token });  
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
        {
            var loginCommand = _mapper.Map<UserLoginCommand>(request);

            try
            {
                var token = await _authService.LoginAsync(loginCommand, cancellationToken);
                return Ok(new { Token = token });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}