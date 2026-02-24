using Frendy.CRM.Shared.Enums;
using Frendy.CRM.Shared.Models;

namespace Frendy.CRM.Shared;

public static class AppState
{
    #region Events

    public static EventHandler<bool>? OnLoadingStateChanged;
    public static EventHandler<NotificationDetails>? OnNotificationCalled;

    #endregion
    
    #region Fields
    
    public static bool LoadingState { get; private set; }
    public static NotificationDetails NotificationDetails { get; set; } = new();
    public static Guid ActionDetailsId { get; set; }
    public static bool ActionModalState { get; set; }
    
    #endregion
    
    #region Methods
    
    public static void ChangeLoadingState(bool isLoading)
    {
        LoadingState = isLoading;
        OnLoadingStateChanged?.Invoke(null, isLoading);
    }
    
    public static async Task CallNotificationAsync(NotificationDetails notificationDetails)
    {
        NotificationDetails = notificationDetails;
        OnNotificationCalled?.Invoke(null, notificationDetails);
        
        await Task.Delay(3000);
        
        NotificationDetails.Show = false;
    }

    public static void CallAuditDetails(Guid actionId)
    {
        ActionDetailsId = actionId;
        ActionModalState = true;
    }

    public static void CloseAuditModal()
    {
        ActionModalState = false;
    } 
    
    #endregion
}