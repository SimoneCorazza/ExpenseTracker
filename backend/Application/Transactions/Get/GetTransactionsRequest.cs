using MediatR;

namespace ExpenseTracker.Application.Transactions.Get;

/// <summary>
///     Gets all transactions for the logged in user
/// </summary>
public class GetTransactionsRequest : IRequest<GetTransactionsResponse>
{
    // TODO: add filtering and paging
}