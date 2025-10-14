namespace ExpenseTracker.Domain.Transactions.Services.TransactionAttachment;

/// <summary>
///     Service to validate the attachments of a transaction
/// </summary>
public interface ITransactionAttachmentService
{
    /// <summary>
    ///     Maximum count of attachments allowed per transaction
    /// </summary>
    int MaxCount { get; }

    /// <summary>
    ///     Maximum size of each attachment in MB
    /// </summary>
    int MaxSizeInMB { get; }

    /// <summary>
    ///     Maximum size of each attachment in bytes
    /// </summary>
    long MaxSizeInBytes { get; }

    /// <summary>
    ///     Lists of mime types allowed for attachments
    /// </summary>
    string[] AllowedTypes { get; }

    /// <summary>
    ///     Validate the attachment from its content
    /// </summary>
    /// <param name="data">Content</param>
    /// <returns>True if valid conent</returns>
    bool ValidateFromContent(byte[] data);
}
