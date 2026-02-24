using Frendy.CRM.Shared.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Frendy.CRM.Components.Buttons;

public partial class NavButton: CustomComponentBase
{
    [Parameter]
    public string Text { get; set; } = null!;

    [Parameter]
    public string? Href { get; set; }

    [Parameter]
    public NavLinkMatch Match { get; set; } = NavLinkMatch.All;
    
    [Parameter]
    public Icons Icon { get; set; }
}