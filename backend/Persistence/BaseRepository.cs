using ExpenseTracker.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence;

public abstract class BaseRepository<T> : IRepository<T>
{
    private readonly DbContext dbContext;

    protected DbContext DbContext => dbContext;

    protected BaseRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public virtual async Task Add(T e)
    {
        dbContext.Add(e ?? throw new ArgumentNullException(nameof(e)));
        await dbContext.SaveChangesAsync();
    }

    public ITransaction BeginTransaction()
    {
        return new EFCoreTransaction(dbContext.Database.BeginTransaction());
    }

    public virtual async Task Update(T e)
    {
        dbContext.Update(e ?? throw new ArgumentNullException(nameof(e)));
        await dbContext.SaveChangesAsync();
    }

    public virtual async Task Delete(T e)
    {
        dbContext.Remove(e ?? throw new ArgumentNullException(nameof(e)));
        await dbContext.SaveChangesAsync();
    }

    /// <summary>
    ///     Scan the collection to find <see cref="EntityState.Detached"/> entities
    ///     and override their state to <see cref="EntityState.Added"/>.
    ///     <br/>
    ///     Useful when updating aggregate roots with new child entities that have a manual ID.
    /// </summary>
    /// <typeparam name="TCollection">Entity type</typeparam>
    /// <param name="collection">Collection of entities to update the state</param>
    protected void TreatDetachedAsAdded<TCollection>(ICollection<TCollection> collection)
    {
        foreach (var c in collection)
        {
            var e = dbContext.Entry(c);
            if (e.State == EntityState.Detached)
            {
                e.State = EntityState.Added;
            }
        }
    }
}
