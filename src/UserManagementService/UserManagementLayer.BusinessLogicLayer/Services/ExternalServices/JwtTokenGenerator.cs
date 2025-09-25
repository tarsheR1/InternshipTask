using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagementService.BusinessLogicLayer.Models.Entities.Users;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;
using UserManagementService.BusinessLogicLayer.Interfaces.Settings;

namespace UserManagementService.BusinessLogicLayer.Services.ExternalServices
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IJwtSettings _jwtSettings;

        public JwtTokenGenerator(IOptions<IJwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings?.Value ?? throw new ArgumentNullException(nameof(jwtSettings));

            if (string.IsNullOrWhiteSpace(_jwtSettings.Secret))
                throw new ArgumentException("JWT Secret is not configured");

            if (_jwtSettings.ExpiryMinutes <= 0)
                throw new ArgumentException("JWT ExpiryMinutes must be positive");
        }

        public string GenerateToken(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var claims = GetUserClaims(user);
            var tokenDescriptor = CreateTokenDescriptor(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private List<Claim> GetUserClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var role in user.UserRoles.Select(ur => ur.Role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            return claims;
        }

        private SecurityTokenDescriptor CreateTokenDescriptor(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };
        }
    }
}