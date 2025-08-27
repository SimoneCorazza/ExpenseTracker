namespace ExpenseTracker.Application.Categories.Edit;

public class CategoryDto
{
    /// <summary>
    ///     Id of the edited category. Null if it's a new category
    /// </summary>
    public Guid? Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public ICollection<CategoryDto> Childrens { get; set; }
}
