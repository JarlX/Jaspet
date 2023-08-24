namespace JaspetWEBUI.Core.ViewModel;

using DTO;

public class ProductViewModel
{
    public List<ProductDTO> Products { get; set; }
    
    public List<CategoryDTO> Categories { get; set; }
}