namespace UserManagementService.BusinessLogicLayer.Models.DTO.Request
{
    public record LoginRequestDto(
        string Email,
        string Password
    );  
}
