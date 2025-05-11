namespace ExpenseTracker.Domain.Categories
{
    public class Category
    {
        /// <summary>
        ///    Category id
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        ///     Parent category id
        /// </summary>
        public Guid? ParentId { get; private set; }

        /// <summary>
        ///    Name of the category
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Description of the category
        /// </summary>
        public string? Description { get; private set; }

        public Category(Guid? parentId, string name, string? description)
        {
            Id = Guid.NewGuid();

            ParentId = parentId;
            Name = name;
            Description = description;

            Validate();
        }

        protected Category()
        {
        }

        public void Update(Guid? parentId, string name, string? description)
        {
            ParentId = parentId;
            Name = name;
            Description = description;

            Validate();
        }

        private void Validate()
        {
            if (Id == ParentId)
            {
                throw new DomainException("The category cannot be parent of itself");
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new DomainException("The name is mandatory");
            }
        }
    }
}
