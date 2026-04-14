using Frendy.CRM.Shared.Enums;

namespace Frendy.CRM.Shared.Models;

public class NotificationItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Message { get; set; } = null!;
    public NotificationType Type { get; set; }
    public bool Show { get; set; }
}