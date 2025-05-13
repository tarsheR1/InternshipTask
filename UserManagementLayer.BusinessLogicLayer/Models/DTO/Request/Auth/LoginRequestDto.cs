namespace UserManagementService.BusinessLogicLayer.Models.DTO.Request.Auth
{
    public sealed record LoginRequestDto(
        string Email,
        string Password
    );  
}
