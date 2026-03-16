using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Buttons;

public partial class TextButton: CustomComponentBase
{
    [Parameter]
    public string Text { get; set; } = null!;

    [Parameter]
    public EventCallback OnClick { get; set; }
    
    [Parameter]
    public string? Icon { get; set; }
    
    [Parameter]
    public bool IsActive { get; set; }
    
    [Parameter]
    public string? Style { get; set; }
    
    [Parameter]
    public bool IsDisabled { get; set; }
}