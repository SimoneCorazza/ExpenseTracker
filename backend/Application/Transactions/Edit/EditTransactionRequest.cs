using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Application.Transactions.Edit;

/// <summary>
///     Edit an existring transaction
/// </summary>
public class EditTransactionRequest : IRequest
{
    /// <summary>
    ///     Id of the transaction to edit
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    ///     Amount of the transaction
    /// </summary>
    [Required]
    public decimal Amount { get; set; }

    /// <summary>
    ///     Optional description of the transaction
    /// </summary>
    [Required]
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
    ///     Optional category id
    /// </summary>
    public Guid? PlaceId { get; set; }
}