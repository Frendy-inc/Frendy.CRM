namespace Frendy.CRM.Shared.Models;

public class UserDetails : UserShortDetailsLookup
{
    public string DeviceName { get; set; } = null!;
    public string City { get; set; } = null!;
    public DateTime Birthday { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Avatar { get; set; }
    public string? Username { get; set; }
    public string IpAddress { get; set; } = null!;
}