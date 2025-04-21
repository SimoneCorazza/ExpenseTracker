using ExpenseTracker.Domain;
using Microsoft.EntityFrameworkCore.Storage;

namespace ExpenseTracker.Persistence
{
    public class EFCoreTransaction : ITransaction
    {
        private readonly IDbContextTransaction dbContextTransaction;

        public EFCoreTransaction(IDbContextTransaction dbContextTransaction)
        {
            this.dbContextTransaction = dbContextTransaction;
        }

        public async Task CommitAsync()
        {
            await dbContextTransaction.CommitAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await dbContextTransaction.DisposeAsync();
            GC.SuppressFinalize(this);
        }

        public async Task RollbackAsync()
        {
            await dbContextTransaction.RollbackAsync();
        }
    }
}
