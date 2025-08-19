namespace ExpenseTracker.Domain.Categories;

public interface ICategoryRepository
{
    /// <summary>
    ///     Get the categories for the given user
    /// </summary>
    /// <param name="userId">User id</param>
    /// <returns>User categories</returns>
    Task<UserCategories> GetFor(Guid userId);

    /// <summary>
    ///     Save the entity
    /// </summary>
    /// <param name="e">Entity</param>
    Task Save(UserCategories userCategories);

    /// <summary>
    ///     Begin a transaction
    /// </summary>
    /// <returns>Transaction object</returns>
    ITransaction BeginTransaction();
}
