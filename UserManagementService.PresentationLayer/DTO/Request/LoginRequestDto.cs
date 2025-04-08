using System.ComponentModel.DataAnnotations;

namespace UserManagementService.PresentationLayer.DTO.Request
{
    public record LoginRequestDto(
        [Required][EmailAddress] string Email,
        [Required][MinLength(6)] string Password
    );  
}
