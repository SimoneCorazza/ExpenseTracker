using ExpenseTracker.Domain;
using ExpenseTracker.Domain.Categories;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence.Categories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DbContext dbContext;

    public CategoryRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<UserCategories> GetFor(Guid userId)
    {
        var categories = await dbContext.Categories
            .Where(c => c.UserId == userId)
            .AsNoTracking()
            .ToArrayAsync();

        var rootCategories = categories.Where(c => c.ParentId is null)
            .ToList();

        var categoryMap = categories
            .Where(c => c.ParentId is not null)
            .GroupBy(c => c.ParentId)
            .ToDictionary(c => (Guid)c.Key!, c => c.ToArray());

        return new UserCategories(userId, [.. rootCategories.Select(x => Convert(x, categoryMap))]);
    }

    public async Task Save(UserCategories e)
    {
        ArgumentNullException.ThrowIfNull(e);

        var categories = e.Categories
            .SelectMany(x => FlattenCategories(x, null, e.UserId))
            .ToArray();
        var ids = categories.Select(c => c.Id).ToArray();

        var transaction = BeginTransaction();

        var categoriesPresent = dbContext.Categories
            .Where(x => x.UserId == e.UserId)
            .Select(x => x.Id)
            .ToHashSet();

        // Udate or insert new categories
        foreach (var c in categories)
        {
            if (categoriesPresent.Contains(c.Id))
            {
                dbContext.Categories.Update(c);
            }
            else
            {
                dbContext.Categories.Add(c);
            }
        }

        // Delete removed categories
        foreach (var id in categoriesPresent.Except(ids))
        {
            var categoryToDelete = new CategoryPersistance { Id = id };
            dbContext.Categories.Attach(categoryToDelete);
            dbContext.Categories.Remove(categoryToDelete);
        }

        dbContext.SaveChanges();

        await transaction.CommitAsync();
    }

    public ITransaction BeginTransaction()
    {
        return new EFCoreTransaction(dbContext.Database.BeginTransaction());
    }

    private static IEnumerable<CategoryPersistance> FlattenCategories(Category category, Guid? parentId, Guid userId)
    {
        var c = new CategoryPersistance
        {
            Id = category.Id,
            UserId = userId,
            Name = category.Name,
            Description = category.Description,
            ParentId = parentId,
        };

        return [c, .. category.Childrens.SelectMany(x => FlattenCategories(x, category.Id, userId))];
    }

    private static Category Convert(CategoryPersistance category, Dictionary<Guid, CategoryPersistance[]> categoryMap)
    {
        return new Category(
            category.Id,
            categoryMap.GetValueOrDefault(category.Id, Array.Empty<CategoryPersistance>()).Select(x => Convert(x, categoryMap)).ToList(),
            category.Name,
            category.Description);
    }
}
