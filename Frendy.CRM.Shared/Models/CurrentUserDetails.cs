using Frendy.Shared.Enums;

namespace Frendy.CRM.Shared.Models;

public class CurrentUserDetails
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string UserName { get; set; } = null!;
    public UserRole Role { get; set; }
    public string? Avatar { get; set; }
}