using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class ModalBase : CustomComponentBase
{
    [Parameter] 
    public bool IsOpen { get; set; }
    [Parameter] 
    public string? Title { get; set; }
    [Parameter] 
    public EventCallback OnClose { get; set; }
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private async Task Close()
    {
        if (OnClose.HasDelegate)
            await OnClose.InvokeAsync();
    }
}