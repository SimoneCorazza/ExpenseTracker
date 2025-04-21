using ExpenseTracker.Application.Services.EmailVerification;
using ExpenseTracker.Application.Services.PasswordEncryptor;
using ExpenseTracker.Application.UserRegistration;
using ExpenseTracker.Domain.Users.Services.PasswordValidator;
using ExpenseTracker.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection") ?? throw new ArgumentNullException();
builder.Services.InitDatabase(connectionString);

builder.Services.AddSingleton<IEmailVerification>(sp => new EmailVerification(builder.Configuration.GetValue<int>("EmailBlobSize")));
builder.Services.AddSingleton<IPasswordEncryptor>(sp => new PasswordEncryptor(builder.Configuration.GetValue<int>("Password:SaltSize")));
builder.Services.AddSingleton<IPasswordValidator>(sp => new PasswordValidator(
    builder.Configuration.GetValue<int>("Password:Validation:MinimumLength"),
    builder.Configuration.GetValue<string>("Password:Validation:SpecialCharactersList")));

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(Assembly.Load("ExpenseTracker.Application"));
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseEntityFramework();

app.UseHttpsRedirection();

app.MapPost("api/v1/user/register", async ([FromBody] RegisterRequest r, IMediator mediator) => await mediator.Send(r));
app.MapGet("api/v1/user/login", () => "Hello World!");

app.Run();