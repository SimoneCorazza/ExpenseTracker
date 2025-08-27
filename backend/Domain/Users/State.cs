namespace ExpenseTracker.Domain.Users;

public enum State
{
    /// <summary>
    ///   User is active
    /// </summary>
    Active = 1,

    /// <summary>
    ///   User is disabled. Cannot do any operation
    /// </summary>
    Disabled = 2,

    /// <summary>
    ///   User is marked for deletion. Cannot do any operation (as Disabled)
    /// </summary>
    ToBeDeleted = 3,
}
