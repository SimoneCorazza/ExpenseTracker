using ExpenseTracker.Domain.Places;
using ExpenseTracker.Domain.Transactions;
using ExpenseTracker.Domain.Users;
using ExpenseTracker.Persistence.Categories;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<Place> Places { get; set; }

    public DbSet<CategoryPersistance> Categories { get; set; }

    public DbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContext).Assembly);
    }
}
