namespace UserManagementService.BusinessLogicLayer.Queries
{
    public sealed record AuthResult(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiry);
}
