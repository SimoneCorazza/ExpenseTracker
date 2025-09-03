using ExpenseTracker.Domain;
using ExpenseTracker.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence.Transactions;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(DbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<ICollection<Transaction>> GetFor(Guid userId)
    {
        return await DbContext.Transactions
            .Where(t => t.UserId == userId)
            .Include(t => t.Attachments)
            .ToListAsync();
    }

    public async Task<Transaction?> GetById(Guid id)
    {
        return await DbContext.Transactions
            .Include(t => t.Attachments)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task Save(Transaction transaction)
    {
        var existingTransaction = await DbContext.Transactions
            .Include(t => t.Attachments)
            .FirstOrDefaultAsync(t => t.Id == transaction.Id);

        if (existingTransaction is null)
        {
            DbContext.Transactions.Add(transaction);
        }
        else
        {
            DbContext.Entry(existingTransaction).CurrentValues.SetValues(transaction);
            
            // Handle attachments
            existingTransaction.Attachments.Clear();
            foreach (var attachment in transaction.Attachments)
            {
                existingTransaction.Attachments.Add(attachment);
            }
        }

        await DbContext.SaveChangesAsync();
    }

    public new async Task Delete(Transaction transaction)
    {
        DbContext.Transactions.Remove(transaction);
        await DbContext.SaveChangesAsync();
    }

    public new ITransaction BeginTransaction()
    {
        return new EFCoreTransaction(DbContext.Database.BeginTransaction());
    }
}
