namespace UserManagementService.BusinessLogicLayer.Models.DTO.Request.Auth
{
    public sealed record RefreshTokenRequestDto
    {
        public string RefreshToken { get; set; }
        public Guid UserId { get; set; }
    }
}
