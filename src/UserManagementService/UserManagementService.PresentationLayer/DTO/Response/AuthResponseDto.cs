namespace UserManagementService.PresentationLayer.DTO.Response
{
    public record AuthResponseDto(
    string AccessToken,
    string RefreshToken,
    int Expires,
    string TokenType = "Bearer");
}
