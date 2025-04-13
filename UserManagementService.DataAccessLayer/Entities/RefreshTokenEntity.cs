using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementService.DataAccessLayer.Entities
{
    [Table("refresh_tokens")]
    public class RefreshTokenEntity
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("token")]
        public string Token { get; set; }

        [Column("expires")]
        public DateTime Expires { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("revoked")]
        public DateTime? Revoked { get; set; }
    }
}
