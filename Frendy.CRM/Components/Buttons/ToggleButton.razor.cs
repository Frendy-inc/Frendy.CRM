using Frendy.CRM.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Buttons;

public partial class ToggleButton: CustomComponentBase
{
    [Parameter]
    public Icons OptionLeft { get; set; }

    [Parameter]
    public Icons OptionRight { get; set; }

    [Parameter]
    public bool IsLeftSelected { get; set; } = true;

    [Parameter]
    public EventCallback<bool> OnToggle { get; set; }

    private async Task ToggleAsync()
    {
        IsLeftSelected = !IsLeftSelected;

        if (OnToggle.HasDelegate)
        {
            await OnToggle.InvokeAsync(IsLeftSelected);
        }
    }
}