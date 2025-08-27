namespace ExpenseTracker.Domain.Places;

/// <summary>
///     Place where the transaction took place
/// </summary>
public class Place
{
    public Place(string name, string? description, Guid userId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        UserId = userId;

        Validate();
    }

    /// <summary>
    ///    Default constructor for EF
    /// </summary>
    protected Place()
    {
    }

    /// <summary>
    ///     Id of this place
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    ///     Name of the place
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    ///    Description of the place
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    ///     User that uses this place
    /// </summary>
    public Guid UserId { get; private set; }

    public void Update(string name, string? description)
    {
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
