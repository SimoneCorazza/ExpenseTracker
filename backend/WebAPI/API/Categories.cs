using ExpenseTracker.Application.Categories.Edit;
using ExpenseTracker.Application.Categories.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebAPI.API;

public static class Categories
{
    public static void MapCategories(this WebApplication app)
    {
        var categories = app.MapGroup("api/v1/categories")
            .RequireAuthorization()
            .WithTags("categories");

        categories.MapGet("/", async (IMediator mediator) => await mediator.Send(new GetCategories()))
           .WithDescription("Gets the categories for the logged in user");
        categories.MapPost("/", async ([FromBody] EditCategoriesRequest r, IMediator mediator) => await mediator.Send(r))
           .WithDescription("Updates the categories for the logged in user");
    }
}
