using MediatR;

namespace ExpenseTracker.Application.Transactions.Edit;

public class EditTransactionRequest : IRequest
{
    /// <summary>
    ///     Id of the transaction to edit
    /// </summary>
    public Guid Id { get; set; }

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