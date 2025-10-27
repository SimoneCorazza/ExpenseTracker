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
}
