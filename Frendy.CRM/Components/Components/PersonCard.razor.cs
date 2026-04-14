using Frendy.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class PersonCard : CustomComponentBase
{
    [Parameter]
    public string? AvatarUrl { get; set; }
    
    [Parameter]
    public string FirstName { get; set; } = null!;
    
    [Parameter]
    public string LastName { get; set; } = null!;
    
    [Parameter]
    public string? Username { get; set; }
    
    [Parameter]
    public UserRole Role { get; set; }
    
    [Parameter]
    public bool IsOnline { get; set; }
    
    [Parameter]
    public bool IsBanned { get; set; }
}