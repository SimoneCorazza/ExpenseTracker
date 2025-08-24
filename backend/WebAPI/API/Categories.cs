using ExpenseTracker.Application.EditCategories;
using ExpenseTracker.Application.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebAPI.API;

public static class Categories
{
    public static void MapCategories(this WebApplication app)
    {
        app.MapGet("api/v1/categories", async (IMediator mediator) => await mediator.Send(new GetCategories()))
           .RequireAuthorization();
        app.MapPost("api/v1/categories", async ([FromBody] EditCategoriesRequest r, IMediator mediator) => await mediator.Send(r))
           .RequireAuthorization();
    }
}
