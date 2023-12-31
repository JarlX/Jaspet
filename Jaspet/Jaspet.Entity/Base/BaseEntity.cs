namespace Jaspet.Entity.Base;

public class BaseEntity
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public bool? IsActive { get; set; } = true;

    public bool? IsDeleted { get; set; } = false;
}