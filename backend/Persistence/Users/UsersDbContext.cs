using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Users
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
                e.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(256);

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
                    .IsRequired(false);

                e.HasIndex(u => u.Email)
                    .IsUnique();
            });
        }
    }
}
