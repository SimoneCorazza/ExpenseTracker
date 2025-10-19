using ExpenseTracker.Application.Users.Login;
using ExpenseTracker.Application.Users.Registration;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebAPI.API;

public static class Users
{
    public static void MapUsers(this WebApplication app)
    {
        var user = app.MapGroup("api/v1/user")
            .WithTags("user");

        user.MapPost("/register", async ([FromBody] RegisterRequest r, IMediator mediator) => await mediator.Send(r))
            .WithDescription("Register a new user");
        user.MapPost("/login", async ([FromBody] UserLoginRequest r, IMediator mediator) => await mediator.Send(r))
            .WithDescription("Log in");
    }
}
