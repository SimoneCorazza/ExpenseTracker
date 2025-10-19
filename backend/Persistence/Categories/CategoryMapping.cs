using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Categories;

internal class CategoryMapping : IEntityTypeConfiguration<CategoryPersistance>
{
    public void Configure(EntityTypeBuilder<CategoryPersistance> builder)
    {
        builder.ToTable("Categories");
        builder.Property(x => x.Id)
            .HasColumnName("category_id");
        builder.HasKey(x => x.Id);


        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ParentId)
            .IsRequired(false);

        // A a user cannot have two categories with the same name under the same parent category.
        builder.HasIndex(x => new { x.UserId, x.Name, x.ParentId })
            .IsUnique()
            .HasDatabaseName("categories_unique_userid_name_parentid");
    }
}
