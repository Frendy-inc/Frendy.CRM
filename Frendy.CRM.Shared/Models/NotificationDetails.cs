using Frendy.CRM.Shared.Enums;

namespace Frendy.CRM.Shared.Models;

public class NotificationDetails
{
    public string Message { get; set; } = null!;
    public NotificationType Type { get; set; }
    public bool Show { get; set; }
}