namespace ExpenseTracker.Application.Transactions.Edit;

public class AttachmentDto
{
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

    /// <summary>
    ///     Object storage id where the attachment is stored
    /// </summary>
    public string ObjectStorageId { get; set; } = string.Empty;
}