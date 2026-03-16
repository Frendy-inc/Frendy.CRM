using Frendy.CRM.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class CopyField : CustomComponentBase
{
    [Parameter]
    public string Label { get; set; } = null!;
    
    [Parameter]
    public string Value { get; set; } = null!;

    private async Task CopyAsync()
    {
        await JsRuntime.InvokeVoidAsync("copyToClipboard", Value);
        
        await AppState.CallNotificationAsync(LocalizationService.GetString("NOTIFY_CLIPBOARD"),
            NotificationType.Positive, show: true);
    }
}