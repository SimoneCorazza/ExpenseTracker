namespace ExpenseTracker.Application.Transactions.Get;

public class TransactionDto
{
    /// <summary>
    ///     Transaction id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Amount of the transaction
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    ///     Description of the transaction
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Date when the transaction took place
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    ///     Category id
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    ///     Place id
    /// </summary>
    public Guid? PlaceId { get; set; }

    /// <summary>
    ///     Attachments of the transaction
    /// </summary>
    public ICollection<AttachmentDto> Attachments { get; set; } = [];
}