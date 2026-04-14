using Frendy.Shared.Enums;

namespace Frendy.CRM.Shared.Models;

public class StaffShortDetailsLookup
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public UserRole Role { get; set; }
    public DateTime LastActivity { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool IsOnline { get; set; }
    public string AssignerFirstName { get; set; } = null!;
    public string AssignerLastName { get; set; } = null!;
    public UserRole AssignerRole { get; set; }
    public DateTime AssignDate  { get; set; }
}