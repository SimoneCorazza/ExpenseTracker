using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Application.Transactions.Delete;

/// <summary>
///     Delets a transaction
/// </summary>
public class DeleteTransactionRequest : IRequest
{
    /// <summary>
    ///     Id of the transaction to delete
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}