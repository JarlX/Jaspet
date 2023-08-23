namespace Jaspet.Entity.Poco;

using Base;

public class User : AuditableEntity
{
    public User()
    {
        Orders = new HashSet<Order>();
    }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string UserRole { get; set; } = "User";

    public virtual IEnumerable<Order> Orders { get; set; }
}