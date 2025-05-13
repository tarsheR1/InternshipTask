namespace UserManagementService.BusinessLogicLayer.Models.DTO.Request
{
    public sealed record LoginRequestDto(
        string Email,
        string Password
    );  
}
