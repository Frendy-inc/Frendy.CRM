using Frendy.Shared.Enums;

namespace Frendy.CRM.Shared.Models;

public class UserShortDetailsLookup
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public UserRole Role { get; set; }
    public DateTime LastActivity { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public AuthType AuthType { get; set; }
    public bool IsBanned { get; set; }
    public bool IsOnline { get; set; }
}