namespace ExpenseTracker.Domain;

/// <summary>
///     Transaction interface
/// </summary>
public interface ITransaction : IAsyncDisposable
{
    /// <summary>
    ///      Commit the transaction
    /// </summary>
    Task CommitAsync();

    /// <summary>
    ///     Rollback the transaction
    /// </summary>
    Task RollbackAsync();
}
