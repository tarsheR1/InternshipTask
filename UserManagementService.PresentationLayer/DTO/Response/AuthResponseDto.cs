namespace UserManagementService.PresentationLayer.DTO.Response
{
    public record AuthResponseDto(
    string AccessToken,
    string RefreshToken,
    string TokenType = "Bearer");
}
