namespace Jaspet.DAL.Concrete.Mapping;

using BaseMap;
using Entity.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductMap : BaseMap<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        builder.HasOne(q => q.Category).WithMany(q => q.Products).HasForeignKey(q => q.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(q => q.Name).HasMaxLength(200).IsRequired();
    }
}