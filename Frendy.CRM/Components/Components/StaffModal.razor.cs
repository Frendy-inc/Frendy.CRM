using Frendy.CRM.Shared.Enums;
using Frendy.CRM.Shared.Extensions;
using Frendy.CRM.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class StaffModal : CustomComponentBase
{
    [Parameter]
    public StaffDetails Model { get; set; } = null!;
    
    private async Task OnDemoteClick()
    {
        var confirmed = await ModalService.OpenAsync<ConfirmModal, bool>(new Dictionary<string, object>
        {
            ["Title"] = LocalizationService.GetString("CONFIRM_MODAL_TITLE"),
            ["Text"] = LocalizationService.GetString("STAFF_CONFIRM_MODAL_MESSAGE", Model.LastName, Model.FirstName, Model.Role.GetLocalization(LocalizationService))
        });

        if (confirmed)
        {
            await LoaderService.RunAsync(async () => await Client.DemoteStaffAsync(Model.Id));
            NotificationService.Show(LocalizationService.GetString("NOTIFY_STAFF_DEMOTED"), NotificationType.Positive);
        }
    }

    private void OnClose()
    {
        ModalService.Close();
    }
}