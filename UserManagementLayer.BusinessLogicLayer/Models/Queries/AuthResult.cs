namespace UserManagementService.BusinessLogicLayer.Models.Queries
{
    public sealed record AuthResult(
    string AccessToken,
    string RefreshToken);
}
