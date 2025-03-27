using Microsoft.AspNetCore.Mvc;
using UserManagementService.BusinessLogicLayer.Services.Interfaces;
using UserManagementService.PresentationLayer.DTO.Request;
using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Services.Interfaces.Infrastructure;

namespace UserManagementService.PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper; 

        public UserController(IUserService userService, IMapper autoMapper)
        {
            _userService = userService;
            _mapper = autoMapper;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId, CancellationToken cancellationToken)
        {   
            var user = await _userService.GetUserByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);            
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(
            Guid userId, 
            [FromBody] UpdateUserRequest request,
            CancellationToken cancellation)
        {
            var userUpdateCommand = _mapper.Map<UserUpdateCommand>(request);

            await _userService.UpdateUserAsync(
                userId,
                userUpdateCommand,
                cancellation
            );

            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId, CancellationToken cancellationToken)
        {

            await _userService.DeleteUserAsync(userId, cancellationToken);
            return NoContent();
        }
    }

}
