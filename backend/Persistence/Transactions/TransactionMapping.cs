using ExpenseTracker.Domain.Places;
using ExpenseTracker.Domain.Transactions;
using ExpenseTracker.Domain.Users;
using ExpenseTracker.Persistence.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Transactions;

internal class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");
        builder.Property(x => x.Id)
            .HasColumnName("transaction_id");
        builder.HasKey(x => x.Id);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Amount)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.Date)
            .IsRequired();

        builder.HasOne<CategoryPersistance>()
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne<Place>()
            .WithMany()
            .HasForeignKey(x => x.PlaceId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.OwnsMany(x => x.Attachments, op =>
        {
            op.WithOwner().HasForeignKey("transaction_id");

            op.ToTable("Attachments");
            op.Property(x => x.Id)
                .HasColumnName("attachment_id");
            op.HasKey(x => x.Id);

            op.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256);

            op.Property(x => x.MimeType)
                .IsRequired()
                .HasMaxLength(256);
        });
    }
}
