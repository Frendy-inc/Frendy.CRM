using Frendy.CRM.Shared.Enums;
using Frendy.CRM.Shared.Models;

namespace Frendy.CRM.Services.Services;

public class NotificationService
{
    public event Action<NotificationDetails>? OnShow;

    public void Show(string message, NotificationType type)
    {
        OnShow?.Invoke(new NotificationDetails
        {
            Message = message,
            Type = type
        });
    }
}