namespace ExpenseTracker.Application.Categories.Get;

/// <summary>
///     Response for getting categories
/// </summary>
public class GetCategoriesResponse
{
    /// <summary>
    ///     Categories of the logged in user
    /// </summary>
    public ICollection<Category> Categories { get; set; }
}
