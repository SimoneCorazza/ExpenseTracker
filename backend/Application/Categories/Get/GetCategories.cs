using MediatR;

namespace ExpenseTracker.Application.Categories.Get;

/// <summary>
///     Gets the categories
/// </summary>
public class GetCategories : IRequest<GetCategoriesResponse>
{
}