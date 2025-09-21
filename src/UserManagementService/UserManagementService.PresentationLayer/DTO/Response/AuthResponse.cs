namespace UserManagementService.PresentationLayer.DTO.Response
{
    public record AuthResponse(
    string AccessToken,
    string RefreshToken,
    int ExpiresInSeconds,
    string TokenType = "Bearer");
}
