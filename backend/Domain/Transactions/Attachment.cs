namespace ExpenseTracker.Domain.Transactions;

public class Attachment
{
    /// <summary>
    ///     Attachment id
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    ///     Name of the attachment
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    ///    Mime type of the attachment
    /// </summary>
    public string MimeType { get; private set; }

    /// <summary>
    ///     Size of the attachment in bytes
    /// </summary>
    public long Size { get; private set; }

    /// <summary>
    ///    Description of the attachment
    /// </summary>
    public string? Description { get; private set; }

    public Attachment(Guid objectId, string name, string? description, string mimeType, long size)
    {
        Id = objectId;
        Name = name;
        Description = description;
        MimeType = mimeType;
        Size = size;

        Validate();
    }

    protected Attachment()
    {
        // Constructor for EF Core
    }

    public void Update(string name, string? description, string mimeType, long size)
    {
        Name = name;
        Description = description;
        MimeType = mimeType;
        Size = size;

        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new DomainException("The name is mandatory");
        }

        if (string.IsNullOrWhiteSpace(MimeType))
        {
            throw new DomainException("The mime type is mandatory");
        }

        if (Size <= 0)
        {
            throw new DomainException("The size must be greater than 0");
        }
    }
}
