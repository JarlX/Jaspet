namespace JaspetWEBUI.Core.ViewModel;

using DTO;

public class HomeViewModel
{
    public List<CategoryDTO> Categories { get; set; }
    
    public List<UserDTO> Users { get; set; }
    
    public List<ProductDTO> Products { get; set; }
}