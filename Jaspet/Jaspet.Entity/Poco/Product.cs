namespace Jaspet.Entity.Poco;

using Base;

public class Product : AuditableEntity
{
    public Product()
    {
        OrderDetails = new HashSet<OrderDetail>();
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public int CategoryId { get; set; }

    public double? UnitPrice { get; set; }

    public virtual Category Category { get; set; }

    public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
}