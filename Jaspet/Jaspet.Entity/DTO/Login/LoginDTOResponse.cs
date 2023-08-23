namespace Jaspet.Entity.DTO.Login;

using System.Text.Json.Serialization;

public class LoginDTOResponse
{
    public string FullName { get; set; }

    public string UserName { get; set; }

    [JsonIgnore] public string Password { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; }

    public string? Email { get; set; }

    public string Address { get; set; }

    public string UserRole { get; set; }
}