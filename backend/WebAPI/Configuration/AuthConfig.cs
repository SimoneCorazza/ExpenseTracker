using ExpenseTracker.Application.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTracker.WebAPI.Configuration;

public static class AuthConfig
{
    private class JwtConfiguration
    {
        public string SigningKey { get; set; }
        public string EncryptionKey { get; set; }
        public string Duration { get; set; }
        public string Issuer { get; set; }
    }

    public static void ConfigAuth(this WebApplicationBuilder builder)
    {
            
        var jwtConfig = builder.Configuration.GetSection("JWT").Get<JwtConfiguration>() ?? throw new ArgumentNullException();
        var signingKey = Convert.FromBase64String(jwtConfig.SigningKey);
        var encryptionKey = Convert.FromBase64String(jwtConfig.EncryptionKey);
        var duration = TimeSpan.Parse(jwtConfig.Duration);
        var issuer = jwtConfig.Issuer;

        builder.Services.AddSingleton<IAuth>(sp => new Auth(signingKey, encryptionKey, issuer, duration));

        builder.Services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.FromSeconds(10),
                    TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey),
                };
            });
    }
}
