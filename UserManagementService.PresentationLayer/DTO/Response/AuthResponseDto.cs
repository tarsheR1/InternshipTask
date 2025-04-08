namespace UserManagementService.PresentationLayer.DTO.Response
{
    public record AuthResponseDto(
    string AccessToken,
    string RefreshToken,
    int ExpiresInMinutes,
    string TokenType = "Bearer");
}
