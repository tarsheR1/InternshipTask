using System.Security.Cryptography;
using UserManagementService.DataAccessLayer.Entities;
using UserManagementService.DataAccessLayer.Interfaces.Repositories;
using UserManagementService.BusinessLogicLayer.Interfaces.Auth;
using UserManagementService.BusinessLogicLayer.Models.Entities.Auth;
using AutoMapper;

namespace UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Auth
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _tokenRepository;
        private readonly TimeSpan _tokenLifetime = TimeSpan.FromDays(30);
        private readonly IMapper _mapper;

        public RefreshTokenService(IRefreshTokenRepository tokenRepository, IMapper mapper)
        {
            _mapper = mapper;
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

            await _tokenRepository.CreateAsync(token, cancellationToken);
            return tokenValue;
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(string token, CancellationToken cancellationToken)
        {
            return  _mapper.Map<RefreshToken>(await _tokenRepository.GetByTokenAsync(token, cancellationToken));
        }

        public async Task<bool> ValidateRefreshTokenAsync(RefreshToken token, CancellationToken cancellationToken)
        {
            return token != null &&
                   token.Revoked == null &&
                   token.Expires > DateTime.UtcNow;
        }

        public async Task RevokeRefreshTokenAsync(string token, CancellationToken cancellationToken)
        {
            var storedToken = await _tokenRepository.GetByTokenAsync(token, cancellationToken);
            if (storedToken != null && storedToken.Revoked == null)
            {
                storedToken.Revoked = DateTime.UtcNow;
                await _tokenRepository.UpdateAsync(storedToken, cancellationToken);
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
