using Microsoft.AspNetCore.Mvc;
using UserManagementService.PresentationLayer.DTO.Request;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Commands;
using AutoMapper;
using Shared.Pagination;
using UserManagementService.BusinessLogicLayer.Models.Queries;
using UserManagementService.DataAccessLayer.Specifications.Users;

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

        [HttpGet]
        public async Task<IActionResult> GetUsersPaginated(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string search = null,
            [FromQuery] bool? isActive = null,
            [FromQuery] List<string> roles = null,
            [FromQuery] DateTime? createdFrom = null,
            [FromQuery] DateTime? createdTo = null,
            [FromQuery] string sortField = "CreatedAt",
            [FromQuery] bool isDescending = true,
            CancellationToken cancellationToken = default)
        {
            var pagination = new PaginationParameters
            { 
                PageNumber = pageNumber, 
                PageSize = pageSize
            };

            var filter = new UserFilter
            {
                Search = search,
                IsActive = isActive,
                Roles = roles ?? new List<string>(),
                CreatedFrom = createdFrom,
                CreatedTo = createdTo
            };
            var sort = new SortOptions 
            { 
                Field = sortField, 
                IsDescending = isDescending 
            };

            var result = await _userService.GetUsersPaginatedAsync(
                pagination,
                filter,
                sort,
                cancellationToken);

            return Ok(result);
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