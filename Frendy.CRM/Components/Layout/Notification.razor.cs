using Frendy.CRM.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Layout;

public partial class Notification : CustomComponentBase
{
    [Parameter]
    public NotificationDetails Details { get; set; } = null!;
}