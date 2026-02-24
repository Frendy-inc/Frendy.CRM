using Frendy.CRM.Shared;
using Frendy.CRM.Shared.Enums;
using Frendy.CRM.Shared.Extensions;
using Frendy.CRM.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Layout;

public partial class AuditModal : CustomComponentBase
{
    [Parameter]
    public Guid ActionId { get; set; }
    
    private AuditDetails AuditDetails { get; set; } = null!;
    private string _iconStatusStyle = null!;

    protected override async Task OnInitializedAsync()
    {
        await GetAuditDetails();
        
        await base.OnInitializedAsync();
    }

    private async Task GetAuditDetails()
    {
        var auditDetails = await Client.GetAuditDetailsAsync(ActionId);
        AuditDetails = auditDetails;
        _iconStatusStyle = $"var(--{AuditDetails.Action.GetActionClass()}-primary-color)";
    }

    private async Task Copy()
    {
        await JsRuntime.InvokeVoidAsync("copyToClipboard", AuditDetails.Id.ToString());
        
        await AppState.CallNotificationAsync(new NotificationDetails
        {
            Message = LocalizationService.GetString("NOTIFY_CLIPBOARD"),
            Show = true,
            Type = NotificationType.Positive
        });
    }

    private void Close()
    {
        AppState.CloseAuditModal();
    }
}