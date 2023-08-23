namespace Jaspet.Entity.DTO.Category;

public class CategoryDTOResponse : CategoryDTOBase
{
    public Guid Guid { get; set; }

    public bool? IsActive { get; set; }
}