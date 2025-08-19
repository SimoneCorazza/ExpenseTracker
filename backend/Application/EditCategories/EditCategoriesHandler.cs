using ExpenseTracker.Application.Services.User;
using ExpenseTracker.Domain.Categories;
using MediatR;

namespace ExpenseTracker.Application.EditCategories;

public class EditCategoriesHandler : IRequestHandler<EditCategoriesRequest>
{
    private readonly IUser user;
    private readonly ICategoryRepository categoryRepository;

    public EditCategoriesHandler(
        IUser user,
        ICategoryRepository categoryRepository)
    {
        this.user = user;
        this.categoryRepository = categoryRepository;
    }

    public async Task Handle(EditCategoriesRequest request, CancellationToken cancellationToken)
    {
        if (user.LoggedUser is null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        Guid userId = user.LoggedUser.UserId;
        var userCategories = await categoryRepository.GetFor(userId);

        if (userCategories is null)
        {
            userCategories = new UserCategories(userId, Categories(request.RootCategories));
        }
        else
        {
            userCategories.Update(Categories(request.RootCategories));
        }

        await categoryRepository.Save(userCategories);
    }

    private static ICollection<Category> Categories(ICollection<CategoryDto> categories)
    {
        if (categories is null || categories.Count == 0)
        {
            return [];
        }

        return [.. categories.Select(x =>
        {
            if (x.Id is null)
            {
                return new Category(Categories(x.Childrens), x.Name, x.Description);
            }
            else
            {
                return new Category((Guid)x.Id, Categories(x.Childrens), x.Name, x.Description);
            }
        })];
    }
}
