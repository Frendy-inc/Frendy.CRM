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
    
    private AuditDetails? AuditDetails { get; set; }
    private string _iconStatusStyle = null!;
    private string _actionTypeClass = null!;

    protected override async Task OnInitializedAsync()
    {
        await GetAuditDetailsAsync();
        
        await base.OnInitializedAsync();
    }

    private async Task GetAuditDetailsAsync()
    {
        var auditDetails = await Client.GetAuditDetailsAsync(ActionId);
        AuditDetails = auditDetails;
        
        if (AuditDetails is not null)
        {
            _iconStatusStyle = $"var(--{AuditDetails.Action.GetStyleClass()}-primary-color)";
            _actionTypeClass = $"status {AuditDetails.Action.GetStyleClass()} bg";
        }
    }

    private async Task CopyAsync()
    {
        await JsRuntime.InvokeVoidAsync("copyToClipboard", AuditDetails!.Id.ToString());
        
        await AppState.CallNotificationAsync(LocalizationService.GetString("NOTIFY_CLIPBOARD"), 
            NotificationType.Positive, show: true);
    }

    private void Close()
    {
        AppState.CloseAuditModal();
    }
}