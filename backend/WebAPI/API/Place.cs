using ExpenseTracker.Application.Place.Get;
using ExpenseTracker.Application.Places.Create;
using ExpenseTracker.Application.Places.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebAPI.API;

public static class Place
{
    public static void MapPlacess(this WebApplication app)
    {
        app.MapGet("api/v1/places", async (IMediator mediator) => await mediator.Send(new GetPlaces()))
           .RequireAuthorization();
        app.MapPost("api/v1/place", async ([FromBody] CreatePlace r, IMediator mediator) => await mediator.Send(r))
           .RequireAuthorization();
        app.MapDelete("api/v1/place", async ([FromBody] DeletePlace r, IMediator mediator) => await mediator.Send(r))
           .RequireAuthorization();
        app.MapPost("api/v1/place/add", async ([FromBody] CreatePlace r, IMediator mediator) => await mediator.Send(r))
           .RequireAuthorization();
    }
}
