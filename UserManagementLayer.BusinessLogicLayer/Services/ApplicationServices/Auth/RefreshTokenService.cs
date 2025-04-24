using System.Security.Cryptography;
using UserManagementService.DataAccessLayer.Entities.Users;
using UserManagementService.BusinessLogicLayer.Interfaces.Auth;
using UserManagementService.BusinessLogicLayer.Exceptions.Token;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Users;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Auth
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _tokenRepository;
        private readonly TimeSpan _tokenLifetime = TimeSpan.FromDays(30);

        public RefreshTokenService(IRefreshTokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task<string> GenerateRefreshTokenAsync(Guid userId, CancellationToken cancellationToken)
        {
            var tokenValue = GenerateSecureToken();

            var token = new RefreshTokenEntity
            {
                Token = tokenValue,
                UserId = userId,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.Add(_tokenLifetime)
            };

            await _tokenRepository.AddAsync(token, cancellationToken);
            return tokenValue;
        }

        public async Task<bool> ValidateRefreshTokenAsync(string token, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new InvalidTokenException("Token cannot be empty");

            var storedToken = await _tokenRepository.GetByTokenAsync(token, cancellationToken);
            return storedToken != null &&
                   storedToken.Revoked == null &&
                   storedToken.Expires > DateTime.UtcNow;
        }

        public async Task RevokeRefreshTokenAsync(string token, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new InvalidTokenException("Token не может быть пустым");

            var storedToken = await _tokenRepository.GetByTokenAsync(token, cancellationToken);

            if (storedToken == null)
                throw new TokenNotFoundException("Refresh token не найден");

            if (storedToken.Revoked != null)
                throw new TokenAlreadyRevokedException("Refresh Token уже отозван");

            storedToken.Revoked = DateTime.UtcNow;
            await _tokenRepository.UpdateAsync(storedToken, cancellationToken);
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