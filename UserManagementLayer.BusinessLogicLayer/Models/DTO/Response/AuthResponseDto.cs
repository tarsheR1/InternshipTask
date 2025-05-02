namespace UserManagementService.BusinessLogicLayer.Models.DTO.Response
{
    public record AuthResponseDto(
    string AccessToken,
    string RefreshToken,
    string TokenType = "Bearer");
}
