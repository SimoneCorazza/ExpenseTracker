namespace ExpenseTracker.Domain.Transactions.Services.TransactionAttachment
{
    public class TransactionAttachmentService : ITransactionAttachmentService
    {
        public TransactionAttachmentService(int maxCount, int maxSizeInMB)
        {
            MaxCount = maxCount;
            MaxSizeInMB = maxSizeInMB;
        }

        public int MaxCount { get; }

        public int MaxSizeInMB { get; }

        public long MaxSizeInBytes => MaxSizeInMB * 1024L * 1024L;
    }
}
