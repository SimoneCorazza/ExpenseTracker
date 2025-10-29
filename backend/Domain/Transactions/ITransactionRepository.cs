namespace ExpenseTracker.Domain.Transactions;

public interface ITransactionRepository : IRepository<Transaction>
{
    /// <summary>
    ///     Get all transactions for the given user
    /// </summary>
    /// <param name="userId">User id</param>
    /// <returns>List of transactions</returns>
    Task<ICollection<Transaction>> GetFor(Guid userId);

    /// <summary>
    ///     Get a transaction by id
    /// </summary>
    /// <param name="id">Transaction id</param>
    /// <returns>Transaction or null if not found</returns>
    Task<Transaction?> GetById(Guid id);

    /// <summary>
    ///     Begin a transaction
    /// </summary>
    /// <returns>Transaction object</returns>
    new ITransaction BeginTransaction();
}
