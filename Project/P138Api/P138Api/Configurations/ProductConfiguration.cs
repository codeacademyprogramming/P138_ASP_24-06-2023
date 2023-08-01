using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P138Api.Entities;

namespace P138Api.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(b => b.CreatedAt).IsRequired(true).HasDefaultValue(DateTime.UtcNow.AddHours(4));
            builder.Property(b => b.UpdatedAt).IsRequired(false);
            builder.Property(b => b.DeletedAt).IsRequired(false);
            builder.Property(b => b.CreatedBy).IsRequired(true).HasMaxLength(255).HasDefaultValue("System");
            builder.Property(b => b.UpdatedBy).IsRequired(false);
            builder.Property(b => b.DeletedBy).IsRequired(false);

            builder
                .HasOne(b => b.Category)
                .WithMany(b => b.Products)
                .HasForeignKey(b => b.CategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
