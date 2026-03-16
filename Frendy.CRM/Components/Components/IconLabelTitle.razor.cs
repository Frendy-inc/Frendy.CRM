using Frendy.CRM.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class IconLabelTitle : ComponentBase
{
    [Parameter]
    public Icons Icon { get; set; }
    
    [Parameter]
    public IconSize Size { get; set; }
    
    [Parameter]
    public string? IconColor { get; set; }
    
    [Parameter]
    public string? IconClass { get; set; }
    
    [Parameter]
    public string Text { get; set; } = null!;
}