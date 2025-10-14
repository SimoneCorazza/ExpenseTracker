namespace ExpenseTracker.Application.Transactions.Get;

public class AttachmentDto
{
    /// <summary>
    ///     Attachment id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Name of the attachment
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Description of the attachment
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Mime type of the attachment
    /// </summary>
    public string MimeType { get; set; } = string.Empty;

    /// <summary>
    ///     Size of the attachment in bytes
    /// </summary>
    public long Size { get; set; }
}