using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P138Api.Entities;

namespace P138Api.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(b => b.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(b => b.Image).IsRequired(false).HasMaxLength(255);
            builder.Property(b=>b.CreatedAt).IsRequired(true).HasDefaultValue(DateTime.UtcNow.AddHours(4));
            builder.Property(b=>b.UpdatedAt).IsRequired(false);
            builder.Property(b=>b.DeletedAt).IsRequired(false);
            builder.Property(b => b.CreatedBy).IsRequired(true).HasMaxLength(255).HasDefaultValue("System");
            builder.Property(b => b.UpdatedBy).IsRequired(false);
            builder.Property(b => b.DeletedBy).IsRequired(false);

            builder
                .HasOne(b=>b.Parent)
                .WithMany(b=>b.Children)
                .HasForeignKey(b=>b.ParentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
