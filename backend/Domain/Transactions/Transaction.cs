using ExpenseTracker.Domain.Transactions.Services.TransactionAttachment;

namespace ExpenseTracker.Domain.Transactions;

public class Transaction
{
    /// <summary>
    ///     Transaction id
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    ///     User that has this transaction
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    ///     Amount
    /// </summary>
    public decimal Amount { get; private set; }

    /// <summary>
    ///     Description
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    ///     When the transaction took place
    /// </summary>
    public DateOnly Date { get; private set; }

    /// <summary>
    ///    Category id
    /// </summary>
    public Guid? CategoryId { get; private set; }

    /// <summary>
    ///     Place id
    /// </summary>
    public Guid? PlaceId { get; private set; }

    /// <summary>
    ///    Attachments of the transaction
    /// </summary>
    public List<Attachment> Attachments { get; private set; }

    public Transaction(
        Guid userId,
        decimal amount,
        string? description,
        DateOnly date,
        Guid? categoryId,
        Guid? placeId,
        ICollection<Attachment> attachments)
    {
        UserId = userId;
        Amount = amount;
        Description = description;
        Date = date;
        CategoryId = categoryId;
        PlaceId = placeId;
        Attachments = attachments.ToList();

        Id = Guid.NewGuid();

        Validate();
    }

    protected Transaction()
    {
    }

    public void Update(
        decimal amount,
        string? description,
        DateOnly date,
        Guid? categoryId,
        Guid? placeId)
    {
        Amount = amount;
        Description = description;
        Date = date;
        CategoryId = categoryId;
        PlaceId = placeId;

        Validate();
    }

    public void Update(ICollection<Attachment> attachments, int maxCount)
    {
        Attachments.AddRange(attachments);

        if (Attachments.Count > maxCount)
        {
            throw new DomainException($"Cannot add more than {maxCount} attachments");
        }

        Validate();
    }

    private void Validate()
    {
        if (Amount == 0)
        {
            throw new DomainException("The amount cannot be 0");
        }

        if (Attachments is not null)
        {
            if (Attachments.Any(x => x is null))
            {
                throw new DomainException("The attachments cannot be null");
            }
        }
    }
}
