using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExpenseTracker.Application.Services.Auth
{
    public class Auth : IAuth
    {
        private const string EmailVerified = "EmailVerified";

        private readonly JwtSecurityTokenHandler tokenHandler;
        private readonly SymmetricSecurityKey signingKey;
        private readonly SymmetricSecurityKey encryptionKey;
        private readonly string issuer;
        private readonly TimeSpan tokenDuration;

        public Auth(byte[] signingKey, byte[] encryptionKey, string issuer, TimeSpan tokenDuration)
        {
            tokenHandler = new JwtSecurityTokenHandler();

            this.signingKey = new SymmetricSecurityKey(signingKey);
            this.encryptionKey = new SymmetricSecurityKey(encryptionKey);
            this.issuer = issuer;
            this.tokenDuration = tokenDuration;
        }

        public AuthenticatedUser FromClaims(ClaimsPrincipal claimsPrincipal)
        {
            return new AuthenticatedUser(
                Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                claimsPrincipal.FindFirst(ClaimTypes.Email).Value,
                bool.Parse(claimsPrincipal.FindFirst(EmailVerified)?.Value));
        }

        public AuthToken GenerateToken(AuthenticatedUser authenticatedUser)
        {
            var expireDate = DateTime.UtcNow.Add(tokenDuration);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, authenticatedUser.UserId.ToString()),
                    new Claim(ClaimTypes.Email, authenticatedUser.Email),
                    new Claim(EmailVerified, authenticatedUser.VerifiedEmail.ToString()),
                ]),
                Expires = expireDate,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature),
                EncryptingCredentials = new EncryptingCredentials(encryptionKey, SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512),
                Issuer = issuer,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthToken
            {
                Token = tokenHandler.WriteToken(token),
                ExpireDate = expireDate,
            };
        }
    }
}
