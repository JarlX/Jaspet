namespace Jaspet.Entity.DTO.Product;

public class ProductDTOBase
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string CategoryName { get; set; }
    public Guid CategoryGUID { get; set; }
    public double? UnitPrice { get; set; }
}