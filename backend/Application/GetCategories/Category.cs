namespace ExpenseTracker.Application.GetCategories;

public class Category
{
    /// <summary>
    ///     Id of this category
    /// </summary>
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public ICollection<Category> Childrens { get; set; }
}
