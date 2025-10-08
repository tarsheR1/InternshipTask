using System.ComponentModel.DataAnnotations;

namespace UserManagementService.BusinessLogicLayer.Models.Entities.Auth
{
    public class RefreshTokenEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [Required, MaxLength(500)]
        public string Token { get; set; }

        [Required]
        public DateTime Expires { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Revoked { get; set; }
    }

}
