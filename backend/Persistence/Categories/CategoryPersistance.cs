namespace ExpenseTracker.Persistence.Categories;

/// <summary>
///     Persistance model for Category
/// </summary>
public class CategoryPersistance
{
    /// <summary>
    ///    Category id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     User that owns this category
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///    Name of the category
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Description of the category
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Owner for this category
    /// </summary>
    public Guid? ParentId { get; set; }
}
