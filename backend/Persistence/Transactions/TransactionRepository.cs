using ExpenseTracker.Domain.Transactions;

namespace ExpenseTracker.Persistence.Transactions;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(DbContext dbContext)
        : base(dbContext)
    {
    }
}
