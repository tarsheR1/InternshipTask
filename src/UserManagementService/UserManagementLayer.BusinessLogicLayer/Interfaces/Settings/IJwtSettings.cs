namespace UserManagementService.BusinessLogicLayer.Interfaces.Settings
{
    public interface IJwtSettings
    {
        string Audience { get; set; }
        int ExpiryMinutes { get; set; }
        string Issuer { get; set; }
        string Secret { get; set; }
    }
}