namespace ExpenseTracker.Domain.Categories;

public class Category
{
    /// <summary>
    ///    Category id
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    ///     Childrens of this category
    /// </summary>
    public ICollection<Category> Childrens { get; private set; }

    /// <summary>
    ///    Name of the category
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    ///     Description of the category
    /// </summary>
    public string? Description { get; private set; }

    public Category(Guid id, ICollection<Category> childrens, string name, string? description)
    {
        Id = id;

        Childrens = childrens;
        Name = name;
        Description = description;

        Validate();
    }

    public Category(ICollection<Category> childrens, string name, string? description)
        : this(Guid.NewGuid(), childrens, name, description)
    {
    }

    protected Category()
    {
    }

    public void Update(ICollection<Category> childrens, string name, string? description)
    {
        Childrens = childrens;
        Name = name;
        Description = description;

        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new DomainException("The name is mandatory");
        }
    }
}
