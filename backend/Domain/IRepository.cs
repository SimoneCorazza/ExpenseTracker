namespace ExpenseTracker.Domain;

/// <summary>
///     Generic repository interface
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T>
{
    /// <summary>
    ///     Add a new entity
    /// </summary>
    /// <param name="e">Entity</param>
    Task Add(T e);

    /// <summary>
    ///     Update an existing entity
    /// </summary>
    /// <param name="e">Entity</param>
    Task Update(T e);

    /// <summary>
    ///     Delete the entity
    /// </summary>
    /// <param name="e"></param>
    Task Delete(T e);

    /// <summary>
    ///     Begin a transaction
    /// </summary>
    /// <returns>Transaction object</returns>
    ITransaction BeginTransaction();
}
