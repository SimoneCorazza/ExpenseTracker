using ExpenseTracker.Domain;

namespace ExpenseTracker.Persistence;

public abstract class BaseRepository<T> : IRepository<T>
{
    private readonly DbContext dbContext;

    protected DbContext DbContext => dbContext;

    protected BaseRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Add(T e)
    {
        dbContext.Add(e);
        dbContext.SaveChanges();
    }

    public ITransaction BeginTransaction()
    {
        return new EFCoreTransaction(dbContext.Database.BeginTransaction());
    }

    public void Update(T e)
    {
        dbContext.Update(e);
        dbContext.SaveChanges();
    }

    public void Delete(T e)
    {
        dbContext.Remove(e);
        dbContext.SaveChanges();
    }
}
