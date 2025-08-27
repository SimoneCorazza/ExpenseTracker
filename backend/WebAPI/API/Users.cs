using ExpenseTracker.Application.Users.Login;
using ExpenseTracker.Application.Users.Registration;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebAPI.API;

public static class Users
{
    public static void MapUsers(this WebApplication app)
    {
        app.MapPost("api/v1/user/register", async ([FromBody] RegisterRequest r, IMediator mediator) => await mediator.Send(r));
        app.MapPost("api/v1/user/login", async ([FromBody] UserLoginRequest r, IMediator mediator) => await mediator.Send(r));
    }
}
