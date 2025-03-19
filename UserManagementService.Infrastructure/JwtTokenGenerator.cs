using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagementService.Core.Models;
using UserManagementService.Infrastructure.Settings;
<<<<<<< Updated upstream
=======
using 
>>>>>>> Stashed changes

namespace UserManagementService.Infrastructure
{
    class JwtTokenGenerator(IOptions<JwtOptions> options) : IJwtTokenGenerator
    {
        JwtOptions _jwtOptions = options.Value;


        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("roles", user.UserRoles.ToString())
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryInMinutes)
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

    }
}
