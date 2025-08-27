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
    void Add(T e);

    /// <summary>
    ///     Update an existing entity
    /// </summary>
    /// <param name="e">Entity</param>
    void Update(T e);

    /// <summary>
    ///     Delete the entity
    /// </summary>
    /// <param name="e"></param>
    void Delete(T e);

    /// <summary>
    ///     Begin a transaction
    /// </summary>
    /// <returns>Transaction object</returns>
    ITransaction BeginTransaction();
}
