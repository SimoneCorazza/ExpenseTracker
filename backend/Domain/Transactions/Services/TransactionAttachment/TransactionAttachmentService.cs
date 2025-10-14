namespace ExpenseTracker.Domain.Transactions.Services.TransactionAttachment
{
    public class TransactionAttachmentService : ITransactionAttachmentService
    {
        private static readonly byte[] pngHeader = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A];
        private static readonly byte[] jpgHeader = [0xFF, 0xD8, 0xFF];
        private static readonly byte[] pdfHeader = [0x25, 0x50, 0x44, 0x2D];

        public TransactionAttachmentService(int maxCount, int maxSizeInMB)
        {
            MaxCount = maxCount;
            MaxSizeInMB = maxSizeInMB;
        }

        public int MaxCount { get; }

        public int MaxSizeInMB { get; }

        public string[] AllowedTypes => ["image/png", "image/jpeg", "application/pdf"];

        public long MaxSizeInBytes => MaxSizeInMB * 1024L * 1024L;

        public bool ValidateFromContent(byte[] data)
        {
            if (data.Length >= pngHeader.Length && data.Take(pngHeader.Length).SequenceEqual(pngHeader))
            {
                return true;
            }
            else if (data.Length >= jpgHeader.Length && data.Take(jpgHeader.Length).SequenceEqual(jpgHeader))
            {
                return true;
            }
            else if (data.Length >= pdfHeader.Length && data.Take(pdfHeader.Length).SequenceEqual(pdfHeader))
            {
                return true;
            }

            return false;
        }
    }
}
