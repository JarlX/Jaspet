namespace Jaspet.Entity.DTO.Product;

public class ProductDTORequest
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public Guid CategoryGUID { get; set; }
    public int CategoryID { get; set; }
    public double? UnitPrice { get; set; }
}