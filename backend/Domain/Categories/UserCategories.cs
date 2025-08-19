namespace ExpenseTracker.Domain.Categories
{
    /// <summary>
    ///   Categories associeted to a given user
    /// </summary>
    public class UserCategories
    {
        public const int MaxLevel = 5;
        public const int MaxNumberOfCategories = 100;

        /// <summary>
        ///    Id of the user
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        ///    Root level categories
        /// </summary>
        public ICollection<Category> Categories { get; private set; }

        public UserCategories(Guid userId, ICollection<Category> categories)
        {
            ArgumentNullException.ThrowIfNull(categories);

            UserId = userId;
            Categories = categories;

            Validate();
        }

        /// <summary>
        ///     Constructor for EF Core
        /// </summary>
        protected UserCategories()
        {
        }

        public void Update(ICollection<Category> categories)
        {
            ArgumentNullException.ThrowIfNull(categories);

            Categories = categories;

            Validate();
        }



        private void Validate()
        {
            if(Categories.Any(c => CheckLevelIsMaximum(c, MaxLevel, 1)))
            {
                throw new DomainException("The maximum level of categories is 5");
            }

            if (CountCategories(Categories) > MaxNumberOfCategories)
            {
                throw new DomainException($"The maximum number of categories is {MaxNumberOfCategories}");
            }
        }

        private static bool CheckLevelIsMaximum(Category c, int maxLevel, int currentLevel)
        {
            if (currentLevel > maxLevel)
            {
                return true;
            }

            return c.Childrens.Any(c => CheckLevelIsMaximum(c, maxLevel, currentLevel + 1));
        }

        private static int CountCategories(ICollection<Category> categories)
        {
            return categories.Count + categories.Sum(c => CountCategories(c.Childrens));
        }
    }
}
