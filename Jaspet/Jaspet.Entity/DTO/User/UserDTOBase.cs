namespace Jaspet.Entity.DTO.User;

using System.Text.Json.Serialization;

public class UserDTOBase
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }
}