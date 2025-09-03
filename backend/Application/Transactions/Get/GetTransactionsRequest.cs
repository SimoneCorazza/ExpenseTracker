using MediatR;

namespace ExpenseTracker.Application.Transactions.Get;

public class GetTransactionsRequest : IRequest<GetTransactionsResponse>
{
}