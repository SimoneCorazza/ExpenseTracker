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

    public override async Task Update(Transaction e)
    {
        TreatDetachedAsAdded(e.Attachments);

        await base.Update(e);
    }
}
