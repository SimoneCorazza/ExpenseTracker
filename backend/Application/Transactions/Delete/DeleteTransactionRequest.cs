using MediatR;

namespace ExpenseTracker.Application.Transactions.Delete;

public class DeleteTransactionRequest : IRequest
{
    /// <summary>
    ///     Id of the transaction to delete
    /// </summary>
    public Guid Id { get; set; }
}