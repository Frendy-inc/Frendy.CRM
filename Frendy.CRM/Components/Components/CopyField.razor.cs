using Frendy.CRM.Services.Services;
using Frendy.CRM.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class CopyField : CustomComponentBase
{
    [Parameter]
    public string Label { get; set; } = null!;
    
    [Parameter]
    public string Value { get; set; } = null!;
    
    [Inject]
    public NotificationService NotificationService { get; set; } = null!;

    private async Task CopyAsync()
    {
        await JsRuntime.InvokeVoidAsync("copyToClipboard", Value);
        
        NotificationService.Show(LocalizationService.GetString("NOTIFY_CLIPBOARD"),
            NotificationType.Positive);
    }
}