using ExpenseTracker.Application.Services.EmailVerification;
using ExpenseTracker.Application.Services.PasswordEncryptor;
using ExpenseTracker.Application.UserLogin;
using ExpenseTracker.Application.UserRegistration;
using ExpenseTracker.Domain.Users.Services.PasswordValidator;
using ExpenseTracker.Persistence;
using ExpenseTracker.WebAPI.Configuration;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

builder.ConfigAuth();
builder.Services.AddCors();
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