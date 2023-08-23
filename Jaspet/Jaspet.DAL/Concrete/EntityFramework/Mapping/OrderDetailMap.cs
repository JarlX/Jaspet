namespace Jaspet.DAL.Concrete.Mapping;

using BaseMap;
using Entity.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderDetailMap : BaseMap<OrderDetail>
{
    public override void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("OrderDetail");
        builder.HasOne(q => q.Product).WithMany(q => q.OrderDetails).HasForeignKey(q => q.OrderID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}