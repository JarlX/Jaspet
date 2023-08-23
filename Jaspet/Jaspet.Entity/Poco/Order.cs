namespace Jaspet.Entity.Poco;

using Base;

public class Order : AuditableEntity
{
    public Order()
    {
        OrderDetails = new HashSet<OrderDetail>();
    }

    public int UserID { get; set; }

    public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }

    public User User { get; set; }
}