using System.ComponentModel.DataAnnotations;

namespace UserManagementService.PresentationLayer.DTO.Request
{
    public sealed record RegisterUserRequestDto
    {
        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; init; }

        [Required, MinLength(8), MaxLength(100)]
        public string Password { get; init; }

        [Required, MaxLength(50)]
        public string FirstName { get; init; }

        [Required, MaxLength(50)]
        public string LastName { get; init; }

        [MaxLength(50)]
        public string? MiddleName { get; init; }

        [Phone, MaxLength(20)]
        public string? Phone { get; init; }
    }
}
