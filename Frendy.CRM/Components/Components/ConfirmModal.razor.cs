using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class ConfirmModal : CustomComponentBase
{
    [Parameter] 
    public string Text { get; set; } = "";

    private void Confirm()
    {
        ModalService.Close(true);
    }

    private void Cancel()
    {
        ModalService.Close(false);
    }
}