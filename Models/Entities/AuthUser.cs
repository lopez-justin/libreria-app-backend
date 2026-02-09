using System.Text.Json.Serialization;

namespace Models.Entities;

public class AuthUser
{
    public int Id { get; set; }
    public int UserId { get; set; }
    [JsonIgnore]
    public virtual User? User { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = "user";
    public bool Active { get; set; }
}