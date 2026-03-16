using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class TextInput : CustomComponentBase
{
    [Parameter]
    public EventCallback<string> OnChange { get; set; }
    
    private void OnInput(ChangeEventArgs e)
    {
        if (OnChange.HasDelegate)
            OnChange.InvokeAsync(e.Value.ToString());
    }
}