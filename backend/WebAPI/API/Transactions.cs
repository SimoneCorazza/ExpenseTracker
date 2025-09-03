using ExpenseTracker.Application.Transactions.Create;
using ExpenseTracker.Application.Transactions.Delete;
using ExpenseTracker.Application.Transactions.Edit;
using ExpenseTracker.Application.Transactions.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebAPI.API;

public static class Transactions
{
    public static void MapTransactions(this WebApplication app)
    {
        app.MapGet("api/v1/transactions", async (IMediator mediator) => 
            await mediator.Send(new GetTransactionsRequest()))
           .RequireAuthorization();

        app.MapPost("api/v1/transactions", async ([FromBody] CreateTransactionRequest request, IMediator mediator) => 
            await mediator.Send(request))
           .RequireAuthorization();

        app.MapPut("api/v1/transactions/{id:guid}", async (Guid id, [FromBody] EditTransactionRequest request, IMediator mediator) =>
        {
            request.Id = id;
            await mediator.Send(request);
            return Results.NoContent();
        })
        .RequireAuthorization();

        app.MapDelete("api/v1/transactions/{id:guid}", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteTransactionRequest { Id = id });
            return Results.NoContent();
        })
        .RequireAuthorization();
    }
}