using Frendy.CRM.Services.Services;
using Frendy.CRM.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class NotificationHost : ComponentBase
{
    [Inject]
    public NotificationService NotificationService { get; set; } = null!;
    
    private readonly List<NotificationItem> _notifications = [];
    
    protected override void OnInitialized()
    {
        NotificationService.OnShow += Show;
    }

    private async void Show(NotificationDetails details)
    {
        var item = new NotificationItem
        {
            Message = details.Message,
            Type = details.Type,
            Show = false
        };

        _notifications.Add(item);
        await InvokeAsync(StateHasChanged);

        await Task.Delay(10);
        item.Show = true;
        await InvokeAsync(StateHasChanged);

        await Task.Delay(3000);

        item.Show = false;
        await InvokeAsync(StateHasChanged);

        await Task.Delay(400);

        _notifications.Remove(item);
        await InvokeAsync(StateHasChanged);
    }
}