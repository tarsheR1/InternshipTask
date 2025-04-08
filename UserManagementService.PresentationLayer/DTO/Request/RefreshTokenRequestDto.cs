namespace UserManagementService.PresentationLayer.DTO.Request
{
    public record RefreshTokenRequestDto
    {
        public string RefreshToken { get; set; }
        public Guid userId { get; set; }
    }
}
