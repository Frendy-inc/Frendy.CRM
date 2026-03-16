using Frendy.CRM.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class IconLabelValue : CustomComponentBase
{
    [Parameter]
    public Icons Icon { get; set; }
    
    [Parameter]
    public IconSize Size { get; set; }
    
    [Parameter]
    public string? Color { get; set; }
    
    [Parameter]
    public string Label { get; set; } = null!;
    
    [Parameter]
    public string? Value { get; set; }
    
    [Parameter]
    public string? ValueClass { get; set; }
    
    [Parameter]
    public string? Description { get; set; }
}