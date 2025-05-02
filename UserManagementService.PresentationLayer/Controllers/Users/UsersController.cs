using Microsoft.AspNetCore.Mvc;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using AutoMapper;
using Shared.Pagination;
using UserManagementService.BusinessLogicLayer.Models.Queries;
using UserManagementService.DataAccessLayer.Specifications.Users;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request;

namespace UserManagementService.PresentationLayer.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
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
            [FromQuery] GetUsersRequestDto request,
            CancellationToken cancellationToken = default)
        {
            
            var pagination = new PaginationParameters
            { 
                PageNumber = request.PageNumber, 
                PageSize = request.PageSize
            };

            var filter = new UserFilter
            {
                Search = request.Search,
                IsActive = request.IsActive,
                Roles = request.Roles,
                CreatedFrom = request.CreatedFrom,
                CreatedTo = request.CreatedTo
            };

            var sort = new SortOptions 
            { 
                Field = request.SortField, 
                IsDescending = request.IsDescending 
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
            await _userService.UpdateUserAsync(userId, request, cancellationToken);
           
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