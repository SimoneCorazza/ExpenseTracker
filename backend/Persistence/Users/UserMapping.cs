using ExpenseTracker.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Users;

internal class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(u => u.Id)
                .HasColumnName("UserId");
            builder.HasKey(u => u.Id);

            builder.OwnsOne(u => u.Email, op =>
            {
                op.Property(u => u.Address)
                    .HasColumnName("EmailAddress")
                    .IsRequired()
                    .HasMaxLength(256);

                op.HasIndex(u => u.Address)
                .IsUnique();
            });

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.PasswordSalt)
                .IsRequired();

            builder.Property(u => u.LastLogIn)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.EmailVerifiedAt)
                .IsRequired(false);

            builder.Property(u => u.EmailVerificationBlob)
                .IsRequired();

            builder.HasIndex(u => u.EmailVerificationBlob)
                .IsUnique();
        }
    }
