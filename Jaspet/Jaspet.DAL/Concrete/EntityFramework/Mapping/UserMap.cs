namespace Jaspet.DAL.Concrete.Mapping;

using BaseMap;
using Entity.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserMap : BaseMap<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.Property(q => q.FirstName).HasMaxLength(30);
        builder.Property(q => q.LastName).HasMaxLength(30);
        builder.Property(q => q.UserName).HasMaxLength(20);
        builder.Property(q => q.Password).HasMaxLength(100);
        builder.Property(q => q.Email).HasMaxLength(30);
        builder.Property(q => q.PhoneNumber).HasMaxLength(20);
        builder.Property(q => q.Address).HasMaxLength(100);
        builder.Property(q => q.UserRole).IsRequired();
    }
}