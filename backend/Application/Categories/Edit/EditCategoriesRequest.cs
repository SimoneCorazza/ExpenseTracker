using MediatR;

namespace ExpenseTracker.Application.Categories.Edit;

/// <summary>
///     Replaces user categories with new ones.
/// </summary>
public class EditCategoriesRequest : IRequest
{
    /// <summary>
    ///     New root categories. These categories will replace the existing ones.
    /// </summary>
    public ICollection<CategoryDto> RootCategories { get; set; }
}
