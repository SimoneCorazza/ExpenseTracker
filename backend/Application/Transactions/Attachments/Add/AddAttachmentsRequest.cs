using MediatR;

namespace ExpenseTracker.Application.Transactions.Attachments.Add;

/// <summary>
///     Add attachments to an existing transaction
/// </summary>
public class AddAttachmentsRequest : IRequest
{
    /// <summary>
    ///     Transaction id to add attachments to
    /// </summary>
    public Guid TransactionId { get; set; }

    /// <summary>
    ///     Attachments to add
    /// </summary>
    public List<AttachmentDto> Attachments { get; set; } = new();
}
