namespace UserManagementService.BusinessLogicLayer.Models.DTO.Request
{
    public record RefreshTokenRequestDto
    {
        public string RefreshToken { get; set; }
        public Guid UserId { get; set; }
    }
}
