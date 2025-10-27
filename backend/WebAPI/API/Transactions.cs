using ExpenseTracker.Application.Transactions.Attachments.Add;
using ExpenseTracker.Application.Transactions.Create;
using ExpenseTracker.Application.Transactions.Delete;
using ExpenseTracker.Application.Transactions.Edit;
using ExpenseTracker.Application.Transactions.Get;
using ExpenseTracker.Domain.Transactions.Services.TransactionAttachment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebAPI.API;

public static class Transactions
{
    public static void MapTransactions(this WebApplication app)
    {
        var transactions = app.MapGroup("api/v1/transactions")
            .RequireAuthorization()
            .WithTags("transactions");

        transactions.MapGet("/", async (IMediator mediator) =>
            await mediator.Send(new GetTransactionsRequest()))
           .WithDescription("Gets the transactions");

        transactions.MapPost("/", async ([FromBody] CreateTransactionRequest request, IMediator mediator) =>
            await mediator.Send(request))
           .WithDescription("Creates a new trnsaction");

        transactions.MapPost("/attachments/{id:guid}", async (
            Guid id,
            IFormFileCollection files,
            ITransactionAttachmentService transactionAttachmentService,
            IMediator mediator) =>
        {   
            await mediator.Send(new AddAttachmentsRequest
            {
                TransactionId = id,
                Attachments = files.Select(x =>
                {
                    using var stream = x.OpenReadStream();
                    using var ms = new MemoryStream();
                    stream.CopyTo(ms);
                    return new Application.Transactions.Attachments.AttachmentDto
                    {
                        Name = x.Name,
                        MimeType = x.ContentType,
                        Data = ms.ToArray(),
                    };
                }).ToList()
            });

            return Results.NoContent();
        })
        .DisableAntiforgery()
        .WithDescription("Adds the given attachments to the transaction");

        transactions.MapPut("/", async ([FromBody] EditTransactionRequest request, IMediator mediator) =>
        {
            await mediator.Send(request);
            return Results.NoContent();
        })
        .WithDescription("Edit an existing transaction");

        transactions.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteTransactionRequest { Id = id });
            return Results.NoContent();
        })
        .WithDescription("Deletes the given trnsaction");
    }
}