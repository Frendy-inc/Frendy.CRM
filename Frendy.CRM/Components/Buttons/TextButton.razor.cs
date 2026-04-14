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
    public string? Color { get; set; }
    
    [Parameter]
    public bool IsDisabled { get; set; }
    
    private string BuildStyle()
    {
        var style = Style ?? "";

        if (!string.IsNullOrEmpty(Color))
        {
            style += $"; --btn-color: {Color};";
        }

        return style;
    }
}