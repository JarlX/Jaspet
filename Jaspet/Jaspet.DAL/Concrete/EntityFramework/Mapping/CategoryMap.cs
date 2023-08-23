namespace Jaspet.DAL.Concrete.Mapping;

using BaseMap;
using Entity.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryMap : BaseMap<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");
        builder.Property(q => q.CategoryName).HasMaxLength(200).IsRequired();
    }
}