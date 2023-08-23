namespace Jaspet.Entity.Poco;

using Base;

public class Category : AuditableEntity
{
    public Category()
    {
        Products = new HashSet<Product>();
    }

    public string CategoryName { get; set; }

    public virtual IEnumerable<Product> Products { get; set; }
}