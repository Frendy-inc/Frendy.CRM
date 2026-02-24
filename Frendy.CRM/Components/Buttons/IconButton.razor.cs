using Frendy.CRM.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Buttons;

public partial class IconButton: CustomComponentBase
{
    [Parameter]
    public Icons? Icon { get; set; }
    
    [Parameter]
    public string? IconUrl { get; set; }
    
    [Parameter]
    public EventCallback OnClick { get; set; }
    
    [Parameter]
    public IconSize Size { get; set; }
    
    [Parameter]
    public string? Color { get; set; }

    private async Task HandleClick()
    {
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync();
        }
    }
}