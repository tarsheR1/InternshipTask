using System.Security.Cryptography;
using UserManagementService.BusinessLogicLayer.Services.Interfaces;
using UserManagementService.DataAccessLayer.Entities;
using UserManagementService.DataAccessLayer.Interfaces;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _tokenRepository;
        private readonly TimeSpan _tokenLifetime = TimeSpan.FromDays(30);

        public RefreshTokenService(IRefreshTokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task<string> GenerateRefreshTokenAsync(Guid userId)
        {
            var tokenValue = GenerateSecureToken();

            var token = new RefreshTokenEntity
            {
                Token = tokenValue,
                UserId = userId,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.Add(_tokenLifetime)
            };

            await _tokenRepository.CreateAsync(token);
            return tokenValue;
        }

        public async Task<bool> ValidateRefreshTokenAsync(string token)
        {
            var storedToken = await _tokenRepository.GetByTokenAsync(token);

            return storedToken != null &&
                   storedToken.Revoked == null &&
                   storedToken.Expires > DateTime.UtcNow;
        }

        public async Task RevokeRefreshTokenAsync(string token)
        {
            var storedToken = await _tokenRepository.GetByTokenAsync(token);
            if (storedToken != null && storedToken.Revoked == null)
            {
                storedToken.Revoked = DateTime.UtcNow;
                await _tokenRepository.UpdateAsync(storedToken);
            }
        }

        private static string GenerateSecureToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }
    }
}
