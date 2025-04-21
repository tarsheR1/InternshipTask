namespace UserManagementService.PresentationLayer.DTO.Request
{
    public record LoginRequestDto(
        string Email,
        string Password
    );  
}
