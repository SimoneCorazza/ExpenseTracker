using MediatR;

namespace ExpenseTracker.Application.Transactions.Create;

public class CreateTransactionRequest : IRequest<CreateTransactionResponse>
{
    /// <summary>
    ///     Amount of the transaction
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    ///     Description of the transaction
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Date when the transaction took place
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    ///     Category id
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    ///     Place id
    /// </summary>
    public Guid? PlaceId { get; set; }
}