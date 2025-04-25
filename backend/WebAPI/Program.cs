using ExpenseTracker.Application.Services.Auth;
using ExpenseTracker.Application.Services.EmailVerification;
using ExpenseTracker.Application.Services.PasswordEncryptor;
using ExpenseTracker.Application.UserLogin;
using ExpenseTracker.Application.UserRegistration;
using ExpenseTracker.Domain.Users.Services.PasswordValidator;
using ExpenseTracker.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection") ?? throw new ArgumentNullException();
builder.Services.InitDatabase(connectionString);

builder.Services.AddSingleton<IEmailVerification>(sp => new EmailVerification(builder.Configuration.GetValue<int>("EmailBlobSize")));
builder.Services.AddSingleton<IPasswordEncryptor>(sp => new PasswordEncryptor(builder.Configuration.GetValue<int>("Password:SaltSize")));
builder.Services.AddSingleton<IPasswordValidator>(sp => new PasswordValidator(
    builder.Configuration.GetValue<int>("Password:Validation:MinimumLength"),
    builder.Configuration.GetValue<string>("Password:Validation:SpecialCharactersList")));

var signingKey = Convert.FromBase64String(builder.Configuration.GetValue<string>("JWT:SigningKey"));
var encryptionKey = Convert.FromBase64String(builder.Configuration.GetValue<string>("JWT:EncryptionKey"));
var duration = TimeSpan.Parse(builder.Configuration.GetValue<string>("JWT:Duration"));
var issuer = builder.Configuration.GetValue<string>("JWT:Issuer");
builder.Services.AddSingleton<IAuth>(sp => new Auth(signingKey, encryptionKey, issuer, duration));

builder.Services.AddCors();

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

builder.Services.AddAuthorization();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("ExpenseTracker.Application")));

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.UseEntityFramework();
app.UseHttpsRedirection();

app.MapPost("api/v1/user/register", async ([FromBody] RegisterRequest r, IMediator mediator) => await mediator.Send(r));
app.MapPost("api/v1/user/login", async ([FromBody] UserLoginRequest r, IMediator mediator) => await mediator.Send(r));

app.MapGet("api/v1/user/test", async (ClaimsPrincipal a) => "aaaa")
   .RequireAuthorization();

app.Run();