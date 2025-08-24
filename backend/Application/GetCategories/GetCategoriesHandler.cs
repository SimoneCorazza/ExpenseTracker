using ExpenseTracker.Application.Services.User;
using ExpenseTracker.Domain.Categories;
using MediatR;

namespace ExpenseTracker.Application.GetCategories;

public class GetCategoriesHandler : IRequestHandler<GetCategories, GetCategoriesResponse>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IUser user;

    public GetCategoriesHandler(ICategoryRepository categoryRepository, IUser user)
    {
        this.user = user;
        this.categoryRepository = categoryRepository;
    }
    public async Task<GetCategoriesResponse> Handle(GetCategories request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetFor(user.LoggedUser.UserId);
        return new GetCategoriesResponse
        {
            Categories = [.. categories.Categories
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Childrens = Map(c.Childrens),
                })],
        };
    }

    private static ICollection<Category> Map(ICollection<Domain.Categories.Category> categories)
    {
        return [.. categories.Select(c => new Category
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Childrens = Map(c.Childrens)
        })];
    }
}
