using ExpenseTracker.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence
{
    public abstract class BaseRepository<T, TDbContext> : IRepository<T> where TDbContext : DbContext
    {
        private readonly TDbContext dbContext;

        protected TDbContext DbContext => dbContext;

        protected BaseRepository(TDbContext dbContext)
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
    }
}
