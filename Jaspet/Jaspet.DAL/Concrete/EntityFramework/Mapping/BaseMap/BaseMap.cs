namespace Jaspet.DAL.Concrete.Mapping.BaseMap;

using Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BaseMap<T> : IEntityTypeConfiguration<T> where T : AuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(q => q.Id);
        builder.Property(q => q.Guid).ValueGeneratedOnAdd();
        builder.Property(q => q.Id).ValueGeneratedOnAdd();
    }
}