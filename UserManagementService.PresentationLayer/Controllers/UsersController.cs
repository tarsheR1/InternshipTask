using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UserManagementService.PresentationLayer.DTO.Request;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;

namespace UserManagementService.PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(
            [FromRoute] Guid userId,
            CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(userId, cancellationToken);
            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(
            [FromRoute] Guid userId,
            [FromBody] UpdateUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UserUpdateCommand>(request);
            await _userService.UpdateUserAsync(userId, command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(
            [FromRoute] Guid userId,
            CancellationToken cancellationToken)
        {
            await _userService.DeleteUserAsync(userId, cancellationToken);
            return NoContent();
        }
    }
}