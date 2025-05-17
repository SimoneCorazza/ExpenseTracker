using ExpenseTracker.Domain.Categories;

namespace ExpenseTracker.Persistence.Categories
{
    internal class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
