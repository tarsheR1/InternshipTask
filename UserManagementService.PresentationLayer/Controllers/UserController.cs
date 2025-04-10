using Microsoft.AspNetCore.Mvc;
using UserManagementService.PresentationLayer.DTO.Request;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace UserManagementService.PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ModerateUsers")]
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
