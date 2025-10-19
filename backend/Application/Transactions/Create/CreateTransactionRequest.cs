using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Application.Transactions.Create;

/// <summary>
///     Request to create a new transaction
/// </summary>
public class CreateTransactionRequest : IRequest<CreateTransactionResponse>
{
    /// <summary>
    ///     Amount of the transaction
    /// </summary>
    [Required]
    public decimal Amount { get; set; }

    /// <summary>
    ///     Optional description of the transaction
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Date when the transaction took place
    /// </summary>
    [Required]
    public DateOnly Date { get; set; }

    /// <summary>
    ///     Optional category id
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    ///     Optional place id
    /// </summary>
    public Guid? PlaceId { get; set; }
}