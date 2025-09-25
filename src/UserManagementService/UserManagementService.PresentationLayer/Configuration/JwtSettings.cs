using UserManagementService.BusinessLogicLayer.Interfaces.Settings;

namespace UserManagementService.BusinessLogicLayer.Models.Settings
{
    public class JwtSettings : IJwtSettings
    {
        public string Secret { get; set; }
        public int ExpiryMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
