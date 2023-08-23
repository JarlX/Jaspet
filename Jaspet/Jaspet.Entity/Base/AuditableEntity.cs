namespace Jaspet.Entity.Base;

public class AuditableEntity : BaseEntity
{
    public DateTime CreatedTime { get; set; }

    public int CreatedUserId { get; set; }

    public string CreatedIPV4Address { get; set; }

    public DateTime UpdatedTime { get; set; }

    public int UpdatedUser { get; set; }

    public string UpdatedIPV4Address { get; set; }
}