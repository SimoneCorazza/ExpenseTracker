using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Application.Categories.Edit;

public class Category
{
    /// <summary>
    ///     Id of the edited category. Null if it's a new category
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    ///     Name of the category
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    ///     Optional description of the category
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Sub categories of this category
    /// </summary>
    public ICollection<Category> Childrens { get; set; }
}
