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

        public AuthenticatedUser ParseToken(string token)
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidateAudience = false,
                ClockSkew = TimeSpan.FromSeconds(10),
                TokenDecryptionKey = encryptionKey,
            }, out SecurityToken validatedToken);

            var userId = Guid.Parse(principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var email = principal.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            var emailVerified = bool.Parse(principal.Claims.First(c => c.Type == EmailVerified).Value);

            return new AuthenticatedUser
            {
                UserId = userId,
                Email = email,
                VerifiedEmail = emailVerified
            };
        }
    }
}
