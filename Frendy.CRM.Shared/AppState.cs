using Frendy.CRM.Shared.Enums;
using Frendy.CRM.Shared.Models;

namespace Frendy.CRM.Shared;

public class AppState
{
    #region Events

    public EventHandler<bool>? OnLoadingStateChanged;
    public EventHandler<NotificationDetails>? OnNotificationCalled;

    #endregion
    
    #region Fields
    
    public bool LoadingState { get; private set; }
    public NotificationDetails NotificationDetails { get; set; } = new();
    public Guid ActionDetailsId { get; private set; }
    public bool ActionModalState { get; private set; }
    public Guid UserDetailsId { get; private set; }
    public bool UserModalState { get; private set; }
    
    #endregion
    
    #region Methods
    
    public void ChangeLoadingState(bool isLoading)
    {
        LoadingState = isLoading;
        OnLoadingStateChanged?.Invoke(null, isLoading);
    }
    
    public async Task CallNotificationAsync(string message, NotificationType type, bool show = false)
    {
        NotificationDetails = new NotificationDetails
        {
            Message = message,
            Type = type,
            Show = show
        };
        OnNotificationCalled?.Invoke(null, NotificationDetails);
        
        await Task.Delay(3000);
        
        NotificationDetails.Show = false;
    }

    public void CallAuditDetails(Guid actionId)
    {
        ActionDetailsId = actionId;
        ActionModalState = true;
    }

    public void CloseAuditModal()
    {
        ActionModalState = false;
    } 

    public void CallUserDetails(Guid userId)
    {
        UserDetailsId = userId;
        UserModalState = true;
    }

    public void CloseUserModal()
    {
        UserModalState = false;
    } 
    
    #endregion
}