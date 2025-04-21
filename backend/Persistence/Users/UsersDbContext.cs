using ExpenseTracker.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence.Users
{
    public class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.OwnsOne(u => u.Email, op =>
                {
                    op.Property(u => u.Address)
                        .HasColumnName("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(256);

                    op.HasIndex(u => u.Address)
                    .IsUnique();
                });

                e.Property(u => u.Id);
                e.HasKey(u => u.Id);

                e.Property(u => u.PasswordHash)
                    .IsRequired();

                e.Property(u => u.PasswordSalt)
                    .IsRequired();

                e.Property(u => u.LastLogIn)
                    .IsRequired();

                e.Property(u => u.CreatedAt)
                    .IsRequired();

                e.Property(u => u.EmailVerifiedAt)
                    .IsRequired(false);

                e.Property(u => u.EmailVerificationBlob)
                    .IsRequired();

                e.HasIndex(u => u.EmailVerificationBlob)
                    .IsUnique();
            });
        }
    }
}
