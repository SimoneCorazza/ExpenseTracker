using ExpenseTracker.Application.Places.Create;
using ExpenseTracker.Application.Places.Delete;
using ExpenseTracker.Application.Places.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebAPI.API;

public static class Places
{
    public static void MapPlacess(this WebApplication app)
    {
        var places = app.MapGroup("api/v1/places")
            .RequireAuthorization()
            .WithTags("places");

        places.MapGet("/", async (IMediator mediator) => await mediator.Send(new GetPlaces()))
           .WithDescription("Gets the places for the logged in user");
        places.MapPost("/", async ([FromBody] CreatePlace r, IMediator mediator) => await mediator.Send(r))
           .WithDescription("Adds a new place for the logged in user");
        places.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) => await mediator.Send(new DeletePlace { Id = id }))
           .WithDescription("Deletes the given place for the logged in user");
    }
}
