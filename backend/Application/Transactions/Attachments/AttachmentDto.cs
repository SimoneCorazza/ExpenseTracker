namespace ExpenseTracker.Application.Transactions.Attachments;

/// <summary>
///     Data for the attachment
/// </summary>
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
    ///     Data of the attachment
    /// </summary>
    public byte[] Data { get; set; } = [];
}