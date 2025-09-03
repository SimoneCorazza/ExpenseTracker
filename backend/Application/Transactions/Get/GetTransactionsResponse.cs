namespace ExpenseTracker.Application.Transactions.Get;

public class GetTransactionsResponse
{
    public ICollection<TransactionDto> Transactions { get; set; } = [];
}