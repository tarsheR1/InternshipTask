namespace UserManagementService.PresentationLayer.DTO.Request
{
    public sealed record LoginRequestDto(
         string Email,
         string Password
    );  
}
